// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Colibri.Networking
{

using global::System;
using global::Google.FlatBuffers;

internal struct Message : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_23_5_26(); }
  public static Message GetRootAsMessage(ByteBuffer _bb) { return GetRootAsMessage(_bb, new Message()); }
  public static Message GetRootAsMessage(ByteBuffer _bb, Message obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public static bool VerifyMessage(ByteBuffer _bb) {Google.FlatBuffers.Verifier verifier = new Google.FlatBuffers.Verifier(_bb); return verifier.VerifyBuffer("", false, MessageVerify.Verify); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Message __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string Channel { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetChannelBytes() { return __p.__vector_as_span<byte>(4, 1); }
#else
  public ArraySegment<byte>? GetChannelBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public byte[] GetChannelArray() { return __p.__vector_as_array<byte>(4); }
  public string Command { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetCommandBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetCommandBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetCommandArray() { return __p.__vector_as_array<byte>(6); }
  public string Payload { get { int o = __p.__offset(8); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetPayloadBytes() { return __p.__vector_as_span<byte>(8, 1); }
#else
  public ArraySegment<byte>? GetPayloadBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public byte[] GetPayloadArray() { return __p.__vector_as_array<byte>(8); }

  public static Offset<Colibri.Networking.Message> CreateMessage(FlatBufferBuilder builder,
      StringOffset channelOffset = default(StringOffset),
      StringOffset commandOffset = default(StringOffset),
      StringOffset payloadOffset = default(StringOffset)) {
    builder.StartTable(3);
    Message.AddPayload(builder, payloadOffset);
    Message.AddCommand(builder, commandOffset);
    Message.AddChannel(builder, channelOffset);
    return Message.EndMessage(builder);
  }

  public static void StartMessage(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddChannel(FlatBufferBuilder builder, StringOffset channelOffset) { builder.AddOffset(0, channelOffset.Value, 0); }
  public static void AddCommand(FlatBufferBuilder builder, StringOffset commandOffset) { builder.AddOffset(1, commandOffset.Value, 0); }
  public static void AddPayload(FlatBufferBuilder builder, StringOffset payloadOffset) { builder.AddOffset(2, payloadOffset.Value, 0); }
  public static Offset<Colibri.Networking.Message> EndMessage(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Colibri.Networking.Message>(o);
  }
  public static void FinishMessageBuffer(FlatBufferBuilder builder, Offset<Colibri.Networking.Message> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedMessageBuffer(FlatBufferBuilder builder, Offset<Colibri.Networking.Message> offset) { builder.FinishSizePrefixed(offset.Value); }
}


static internal class MessageVerify
{
  static public bool Verify(Google.FlatBuffers.Verifier verifier, uint tablePos)
  {
    return verifier.VerifyTableStart(tablePos)
      && verifier.VerifyString(tablePos, 4 /*Channel*/, false)
      && verifier.VerifyString(tablePos, 6 /*Command*/, false)
      && verifier.VerifyString(tablePos, 8 /*Payload*/, false)
      && verifier.VerifyTableEnd(tablePos);
  }
}

}
