// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: protocolFrame.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from protocolFrame.proto</summary>
public static partial class ProtocolFrameReflection {

  #region Descriptor
  /// <summary>File descriptor for protocolFrame.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static ProtocolFrameReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "ChNwcm90b2NvbEZyYW1lLnByb3RvIhsKC1NDU3RhcnRHYW1lEgwKBHVpbnMY",
          "ASADKAQiVgoNU0NGcmFtZU5vdGlmeRIUCgxjdXJyZW50RnJhbWUYASABKA0S",
          "EQoJbmV4dEZyYW1lGAIgASgNEhwKBGtleXMYAyADKAsyDi5DU0ZyYW1lTm90",
          "aWZ5Ih4KD1NDUmVzcG9uc2VTdGFydBILCgN1aW4YASABKAQiKgoNQ1NGcmFt",
          "ZU5vdGlmeRILCgN1aW4YASABKAQSDAoEa2V5cxgCIAMoDSIQCg5DU1JlcXVl",
          "c3RTdGFydGIGcHJvdG8z"));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::SCStartGame), global::SCStartGame.Parser, new[]{ "Uins" }, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::SCFrameNotify), global::SCFrameNotify.Parser, new[]{ "CurrentFrame", "NextFrame", "Keys" }, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::SCResponseStart), global::SCResponseStart.Parser, new[]{ "Uin" }, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::CSFrameNotify), global::CSFrameNotify.Parser, new[]{ "Uin", "Keys" }, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::CSRequestStart), global::CSRequestStart.Parser, null, null, null, null)
        }));
  }
  #endregion

}
#region Messages
public sealed partial class SCStartGame : pb::IMessage<SCStartGame> {
  private static readonly pb::MessageParser<SCStartGame> _parser = new pb::MessageParser<SCStartGame>(() => new SCStartGame());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<SCStartGame> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::ProtocolFrameReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public SCStartGame() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public SCStartGame(SCStartGame other) : this() {
    uins_ = other.uins_.Clone();
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public SCStartGame Clone() {
    return new SCStartGame(this);
  }

  /// <summary>Field number for the "uins" field.</summary>
  public const int UinsFieldNumber = 1;
  private static readonly pb::FieldCodec<ulong> _repeated_uins_codec
      = pb::FieldCodec.ForUInt64(10);
  private readonly pbc::RepeatedField<ulong> uins_ = new pbc::RepeatedField<ulong>();
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public pbc::RepeatedField<ulong> Uins {
    get { return uins_; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as SCStartGame);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(SCStartGame other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if(!uins_.Equals(other.uins_)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    hash ^= uins_.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    uins_.WriteTo(output, _repeated_uins_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    size += uins_.CalculateSize(_repeated_uins_codec);
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(SCStartGame other) {
    if (other == null) {
      return;
    }
    uins_.Add(other.uins_);
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 10:
        case 8: {
          uins_.AddEntriesFrom(input, _repeated_uins_codec);
          break;
        }
      }
    }
  }

}

public sealed partial class SCFrameNotify : pb::IMessage<SCFrameNotify> {
  private static readonly pb::MessageParser<SCFrameNotify> _parser = new pb::MessageParser<SCFrameNotify>(() => new SCFrameNotify());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<SCFrameNotify> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::ProtocolFrameReflection.Descriptor.MessageTypes[1]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public SCFrameNotify() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public SCFrameNotify(SCFrameNotify other) : this() {
    currentFrame_ = other.currentFrame_;
    nextFrame_ = other.nextFrame_;
    keys_ = other.keys_.Clone();
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public SCFrameNotify Clone() {
    return new SCFrameNotify(this);
  }

  /// <summary>Field number for the "currentFrame" field.</summary>
  public const int CurrentFrameFieldNumber = 1;
  private uint currentFrame_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public uint CurrentFrame {
    get { return currentFrame_; }
    set {
      currentFrame_ = value;
    }
  }

  /// <summary>Field number for the "nextFrame" field.</summary>
  public const int NextFrameFieldNumber = 2;
  private uint nextFrame_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public uint NextFrame {
    get { return nextFrame_; }
    set {
      nextFrame_ = value;
    }
  }

  /// <summary>Field number for the "keys" field.</summary>
  public const int KeysFieldNumber = 3;
  private static readonly pb::FieldCodec<global::CSFrameNotify> _repeated_keys_codec
      = pb::FieldCodec.ForMessage(26, global::CSFrameNotify.Parser);
  private readonly pbc::RepeatedField<global::CSFrameNotify> keys_ = new pbc::RepeatedField<global::CSFrameNotify>();
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public pbc::RepeatedField<global::CSFrameNotify> Keys {
    get { return keys_; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as SCFrameNotify);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(SCFrameNotify other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (CurrentFrame != other.CurrentFrame) return false;
    if (NextFrame != other.NextFrame) return false;
    if(!keys_.Equals(other.keys_)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (CurrentFrame != 0) hash ^= CurrentFrame.GetHashCode();
    if (NextFrame != 0) hash ^= NextFrame.GetHashCode();
    hash ^= keys_.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (CurrentFrame != 0) {
      output.WriteRawTag(8);
      output.WriteUInt32(CurrentFrame);
    }
    if (NextFrame != 0) {
      output.WriteRawTag(16);
      output.WriteUInt32(NextFrame);
    }
    keys_.WriteTo(output, _repeated_keys_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (CurrentFrame != 0) {
      size += 1 + pb::CodedOutputStream.ComputeUInt32Size(CurrentFrame);
    }
    if (NextFrame != 0) {
      size += 1 + pb::CodedOutputStream.ComputeUInt32Size(NextFrame);
    }
    size += keys_.CalculateSize(_repeated_keys_codec);
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(SCFrameNotify other) {
    if (other == null) {
      return;
    }
    if (other.CurrentFrame != 0) {
      CurrentFrame = other.CurrentFrame;
    }
    if (other.NextFrame != 0) {
      NextFrame = other.NextFrame;
    }
    keys_.Add(other.keys_);
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 8: {
          CurrentFrame = input.ReadUInt32();
          break;
        }
        case 16: {
          NextFrame = input.ReadUInt32();
          break;
        }
        case 26: {
          keys_.AddEntriesFrom(input, _repeated_keys_codec);
          break;
        }
      }
    }
  }

}

public sealed partial class SCResponseStart : pb::IMessage<SCResponseStart> {
  private static readonly pb::MessageParser<SCResponseStart> _parser = new pb::MessageParser<SCResponseStart>(() => new SCResponseStart());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<SCResponseStart> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::ProtocolFrameReflection.Descriptor.MessageTypes[2]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public SCResponseStart() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public SCResponseStart(SCResponseStart other) : this() {
    uin_ = other.uin_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public SCResponseStart Clone() {
    return new SCResponseStart(this);
  }

  /// <summary>Field number for the "uin" field.</summary>
  public const int UinFieldNumber = 1;
  private ulong uin_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ulong Uin {
    get { return uin_; }
    set {
      uin_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as SCResponseStart);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(SCResponseStart other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Uin != other.Uin) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (Uin != 0UL) hash ^= Uin.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (Uin != 0UL) {
      output.WriteRawTag(8);
      output.WriteUInt64(Uin);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (Uin != 0UL) {
      size += 1 + pb::CodedOutputStream.ComputeUInt64Size(Uin);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(SCResponseStart other) {
    if (other == null) {
      return;
    }
    if (other.Uin != 0UL) {
      Uin = other.Uin;
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 8: {
          Uin = input.ReadUInt64();
          break;
        }
      }
    }
  }

}

public sealed partial class CSFrameNotify : pb::IMessage<CSFrameNotify> {
  private static readonly pb::MessageParser<CSFrameNotify> _parser = new pb::MessageParser<CSFrameNotify>(() => new CSFrameNotify());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<CSFrameNotify> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::ProtocolFrameReflection.Descriptor.MessageTypes[3]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public CSFrameNotify() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public CSFrameNotify(CSFrameNotify other) : this() {
    uin_ = other.uin_;
    keys_ = other.keys_.Clone();
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public CSFrameNotify Clone() {
    return new CSFrameNotify(this);
  }

  /// <summary>Field number for the "uin" field.</summary>
  public const int UinFieldNumber = 1;
  private ulong uin_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ulong Uin {
    get { return uin_; }
    set {
      uin_ = value;
    }
  }

  /// <summary>Field number for the "keys" field.</summary>
  public const int KeysFieldNumber = 2;
  private static readonly pb::FieldCodec<uint> _repeated_keys_codec
      = pb::FieldCodec.ForUInt32(18);
  private readonly pbc::RepeatedField<uint> keys_ = new pbc::RepeatedField<uint>();
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public pbc::RepeatedField<uint> Keys {
    get { return keys_; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as CSFrameNotify);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(CSFrameNotify other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Uin != other.Uin) return false;
    if(!keys_.Equals(other.keys_)) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (Uin != 0UL) hash ^= Uin.GetHashCode();
    hash ^= keys_.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (Uin != 0UL) {
      output.WriteRawTag(8);
      output.WriteUInt64(Uin);
    }
    keys_.WriteTo(output, _repeated_keys_codec);
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (Uin != 0UL) {
      size += 1 + pb::CodedOutputStream.ComputeUInt64Size(Uin);
    }
    size += keys_.CalculateSize(_repeated_keys_codec);
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(CSFrameNotify other) {
    if (other == null) {
      return;
    }
    if (other.Uin != 0UL) {
      Uin = other.Uin;
    }
    keys_.Add(other.keys_);
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 8: {
          Uin = input.ReadUInt64();
          break;
        }
        case 18:
        case 16: {
          keys_.AddEntriesFrom(input, _repeated_keys_codec);
          break;
        }
      }
    }
  }

}

public sealed partial class CSRequestStart : pb::IMessage<CSRequestStart> {
  private static readonly pb::MessageParser<CSRequestStart> _parser = new pb::MessageParser<CSRequestStart>(() => new CSRequestStart());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<CSRequestStart> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::ProtocolFrameReflection.Descriptor.MessageTypes[4]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public CSRequestStart() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public CSRequestStart(CSRequestStart other) : this() {
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public CSRequestStart Clone() {
    return new CSRequestStart(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as CSRequestStart);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(CSRequestStart other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(CSRequestStart other) {
    if (other == null) {
      return;
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
      }
    }
  }

}

#endregion


#endregion Designer generated code
