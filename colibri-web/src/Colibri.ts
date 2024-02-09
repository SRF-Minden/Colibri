import { Socket, connect } from 'socket.io-client';
import ColibriError from './ColibriError';
import { Subject } from 'rxjs';

export interface Message {
    channel: string;
    command: string;
    payload: unknown;
}

export default class Colibri {
    private static instance: Colibri | null = null;

    private readonly socket: Socket;
    public readonly messageSubject = new Subject<Message>();
    public readonly messages = this.messageSubject.asObservable();
    public readonly uri: string;
    public readonly uriRestApi: string;

    public constructor(
        public readonly app: string,
        public readonly server: string = window?.location?.hostname ?? '',
        public readonly port: number = 9011,
        public readonly version: string = '1'
    ) {
        if (server.trim().length <= 0) {
            throw new ColibriError('Server Address missing or empty!');
        }

        if (port < 1 || port > 65535) {
            throw new ColibriError('Port out of allowed range (0 - 65535)');
        }

        this.uri = `${server}:${port}`;
        if (!new RegExp('https?://', 'i').test(this.uri)) {
            this.uri = `http://${this.uri}`;
        }
        this.uriRestApi = `${this.uri}/api/store/${app}/`;

        // there is already an instance running
        // do we actually need a second ?!
        if (Colibri.instance)
            throw new ColibriError('A Colibri instance already exists!');
        else Colibri.instance = this;

        this.socket = connect(this.uri, {
            query: { app, version: version },
        });
        this.socket.on('connect', this.onSocketConnect.bind(this));
        this.socket.onAny(this.onSocketAny.bind(this));
    }

    private onSocketConnect() {
        console.debug(`Connected to colibri server on ${this.server}`);
    }

    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    private onSocketAny(channel: string, msg: any) {
        this.messageSubject.next({
            channel,
            command: msg.command,
            payload: msg.payload,
        });
    }

    /**
     * Returns the existing Colibri instance or null if there's none yet
     * @returns
     */
    public static getInstance(): Colibri | null {
        return Colibri.instance;
    }

    /**
     * Sends a single message in the given `channel`.
     * @param channel message channel
     * @param command message command
     * @param payload message payload
     */
    public sendMessage(
        channel: string,
        command: string,
        payload: unknown = {}
    ) {
        this.socket.emit(channel, {
            command,
            payload,
        });
    }

    //#region Socket Events
    /**
     * Adds a `handler` function listening for messages in `channel`.
     * @param channel message channel
     * @param handler handler to be executed when a message is received
     */
    public registerChannel(
        channel: string,
        handler: (payload: Message) => void
    ) {
        this.socket.on(channel, handler);
    }

    /**
     * Removes the `handler` function listening for messages in `channel`.
     * @param channel message channel
     * @param handler handler to be executed when a message is received
     */
    public unregisterChannel(
        channel: string,
        handler: (payload: Message) => void
    ) {
        this.socket.off(channel, handler);
    }

    /**
     * Adds a one-time `handler` function listening for the next message in `channel`.
     * @param channel message channel
     * @param handler handler to be executed when a message is received
     */
    public registerOnce(channel: string, handler: (payload: Message) => void) {
        this.socket.once(channel, handler);
    }
    //#endregion

    //#region Rest API
    /**
     * Returns the REST API Endpoint for a given `key` or null of the key is empty.
     * @param key REST API storage key
     * @returns
     */
    public getRestUri(key: string): string | null {
        key = key.trim();
        while (key.startsWith('/')) key = key.substring(1);
        return key.length === 0 ? null : `${this.uriRestApi}/${key}`;
    }
    /**
     * Queries an object from the REST API identified by `key`.
     * @param key REST API storage key
     * @returns JSON object with data or null if object does not exist or any other error occurrs
     */
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    public async getRestObject(key: string): Promise<any | null> {
        const uri = this.getRestUri(key);
        if (!uri) return null;

        const response = await fetch(uri, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
        });

        if (response.status >= 400) return null;
        else return response.json();
    }

    /**
     * Sets or updatees an object in the REST API identified by `key`.
     * @param key REST API storage key
     * @param data JSON data to write
     * @returns true if data was written to REST API
     */
    public async setRestObject(key: string, data: unknown): Promise<boolean> {
        const uri = this.getRestUri(key);
        if (!uri) return false;

        const response = await fetch(uri, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        });

        return response.status > 200 && response.status < 300;
    }
    //#endregion
}
