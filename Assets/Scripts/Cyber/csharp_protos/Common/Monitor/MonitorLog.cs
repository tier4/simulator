// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: modules/common/monitor_log/proto/monitor_log.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Apollo.Common.Monitor {

  /// <summary>Holder for reflection information generated from modules/common/monitor_log/proto/monitor_log.proto</summary>
  public static partial class MonitorLogReflection {

    #region Descriptor
    /// <summary>File descriptor for modules/common/monitor_log/proto/monitor_log.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static MonitorLogReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CjJtb2R1bGVzL2NvbW1vbi9tb25pdG9yX2xvZy9wcm90by9tb25pdG9yX2xv",
            "Zy5wcm90bxIVYXBvbGxvLmNvbW1vbi5tb25pdG9yGiFtb2R1bGVzL2NvbW1v",
            "bi9wcm90by9oZWFkZXIucHJvdG8ipwQKEk1vbml0b3JNZXNzYWdlSXRlbRJH",
            "CgZzb3VyY2UYASABKA4yNy5hcG9sbG8uY29tbW9uLm1vbml0b3IuTW9uaXRv",
            "ck1lc3NhZ2VJdGVtLk1lc3NhZ2VTb3VyY2USCwoDbXNnGAIgASgJEkUKCWxv",
            "Z19sZXZlbBgDIAEoDjIyLmFwb2xsby5jb21tb24ubW9uaXRvci5Nb25pdG9y",
            "TWVzc2FnZUl0ZW0uTG9nTGV2ZWwivQIKDU1lc3NhZ2VTb3VyY2USFwoTTUVT",
            "U0FHRVNPVVJDRV9EVU1NWRAAEgsKB1VOS05PV04QARIKCgZDQU5CVVMQAhIL",
            "CgdDT05UUk9MEAMSDAoIREVDSVNJT04QBBIQCgxMT0NBTElaQVRJT04QBRIM",
            "CghQTEFOTklORxAGEg4KClBSRURJQ1RJT04QBxINCglTSU1VTEFUT1IQCBIJ",
            "CgVIV1NZUxAJEgsKB1JPVVRJTkcQChILCgdNT05JVE9SEAsSBwoDSE1JEAwS",
            "EAoMUkVMQVRJVkVfTUFQEA0SCAoER05TUxAOEg8KC0NPTlRJX1JBREFSEA8S",
            "EQoNUkFDT0JJVF9SQURBUhAQEhQKEFVMVFJBU09OSUNfUkFEQVIQERIMCghN",
            "T0JJTEVZRRASEg4KCkRFTFBISV9FU1IQEyI0CghMb2dMZXZlbBIICgRJTkZP",
            "EAASCAoEV0FSThABEgkKBUVSUk9SEAISCQoFRkFUQUwQAyJwCg5Nb25pdG9y",
            "TWVzc2FnZRIlCgZoZWFkZXIYASABKAsyFS5hcG9sbG8uY29tbW9uLkhlYWRl",
            "chI3CgRpdGVtGAIgAygLMikuYXBvbGxvLmNvbW1vbi5tb25pdG9yLk1vbml0",
            "b3JNZXNzYWdlSXRlbWIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Apollo.Common.HeaderReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Apollo.Common.Monitor.MonitorMessageItem), global::Apollo.Common.Monitor.MonitorMessageItem.Parser, new[]{ "Source", "Msg", "LogLevel" }, null, new[]{ typeof(global::Apollo.Common.Monitor.MonitorMessageItem.Types.MessageSource), typeof(global::Apollo.Common.Monitor.MonitorMessageItem.Types.LogLevel) }, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Apollo.Common.Monitor.MonitorMessage), global::Apollo.Common.Monitor.MonitorMessage.Parser, new[]{ "Header", "Item" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class MonitorMessageItem : pb::IMessage<MonitorMessageItem> {
    private static readonly pb::MessageParser<MonitorMessageItem> _parser = new pb::MessageParser<MonitorMessageItem>(() => new MonitorMessageItem());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<MonitorMessageItem> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Apollo.Common.Monitor.MonitorLogReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MonitorMessageItem() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MonitorMessageItem(MonitorMessageItem other) : this() {
      source_ = other.source_;
      msg_ = other.msg_;
      logLevel_ = other.logLevel_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MonitorMessageItem Clone() {
      return new MonitorMessageItem(this);
    }

    /// <summary>Field number for the "source" field.</summary>
    public const int SourceFieldNumber = 1;
    private global::Apollo.Common.Monitor.MonitorMessageItem.Types.MessageSource source_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Apollo.Common.Monitor.MonitorMessageItem.Types.MessageSource Source {
      get { return source_; }
      set {
        source_ = value;
      }
    }

    /// <summary>Field number for the "msg" field.</summary>
    public const int MsgFieldNumber = 2;
    private string msg_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Msg {
      get { return msg_; }
      set {
        msg_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "log_level" field.</summary>
    public const int LogLevelFieldNumber = 3;
    private global::Apollo.Common.Monitor.MonitorMessageItem.Types.LogLevel logLevel_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Apollo.Common.Monitor.MonitorMessageItem.Types.LogLevel LogLevel {
      get { return logLevel_; }
      set {
        logLevel_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as MonitorMessageItem);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(MonitorMessageItem other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Source != other.Source) return false;
      if (Msg != other.Msg) return false;
      if (LogLevel != other.LogLevel) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Source != 0) hash ^= Source.GetHashCode();
      if (Msg.Length != 0) hash ^= Msg.GetHashCode();
      if (LogLevel != 0) hash ^= LogLevel.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Source != 0) {
        output.WriteRawTag(8);
        output.WriteEnum((int) Source);
      }
      if (Msg.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Msg);
      }
      if (LogLevel != 0) {
        output.WriteRawTag(24);
        output.WriteEnum((int) LogLevel);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Source != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Source);
      }
      if (Msg.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Msg);
      }
      if (LogLevel != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) LogLevel);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(MonitorMessageItem other) {
      if (other == null) {
        return;
      }
      if (other.Source != 0) {
        Source = other.Source;
      }
      if (other.Msg.Length != 0) {
        Msg = other.Msg;
      }
      if (other.LogLevel != 0) {
        LogLevel = other.LogLevel;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            source_ = (global::Apollo.Common.Monitor.MonitorMessageItem.Types.MessageSource) input.ReadEnum();
            break;
          }
          case 18: {
            Msg = input.ReadString();
            break;
          }
          case 24: {
            logLevel_ = (global::Apollo.Common.Monitor.MonitorMessageItem.Types.LogLevel) input.ReadEnum();
            break;
          }
        }
      }
    }

    #region Nested types
    /// <summary>Container for nested types declared in the MonitorMessageItem message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static partial class Types {
      public enum MessageSource {
        [pbr::OriginalName("MESSAGESOURCE_DUMMY")] Dummy = 0,
        [pbr::OriginalName("UNKNOWN")] Unknown = 1,
        [pbr::OriginalName("CANBUS")] Canbus = 2,
        [pbr::OriginalName("CONTROL")] Control = 3,
        [pbr::OriginalName("DECISION")] Decision = 4,
        [pbr::OriginalName("LOCALIZATION")] Localization = 5,
        [pbr::OriginalName("PLANNING")] Planning = 6,
        [pbr::OriginalName("PREDICTION")] Prediction = 7,
        [pbr::OriginalName("SIMULATOR")] Simulator = 8,
        [pbr::OriginalName("HWSYS")] Hwsys = 9,
        [pbr::OriginalName("ROUTING")] Routing = 10,
        [pbr::OriginalName("MONITOR")] Monitor = 11,
        [pbr::OriginalName("HMI")] Hmi = 12,
        [pbr::OriginalName("RELATIVE_MAP")] RelativeMap = 13,
        [pbr::OriginalName("GNSS")] Gnss = 14,
        [pbr::OriginalName("CONTI_RADAR")] ContiRadar = 15,
        [pbr::OriginalName("RACOBIT_RADAR")] RacobitRadar = 16,
        [pbr::OriginalName("ULTRASONIC_RADAR")] UltrasonicRadar = 17,
        [pbr::OriginalName("MOBILEYE")] Mobileye = 18,
        [pbr::OriginalName("DELPHI_ESR")] DelphiEsr = 19,
      }

      public enum LogLevel {
        [pbr::OriginalName("INFO")] Info = 0,
        [pbr::OriginalName("WARN")] Warn = 1,
        [pbr::OriginalName("ERROR")] Error = 2,
        [pbr::OriginalName("FATAL")] Fatal = 3,
      }

    }
    #endregion

  }

  public sealed partial class MonitorMessage : pb::IMessage<MonitorMessage> {
    private static readonly pb::MessageParser<MonitorMessage> _parser = new pb::MessageParser<MonitorMessage>(() => new MonitorMessage());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<MonitorMessage> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Apollo.Common.Monitor.MonitorLogReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MonitorMessage() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MonitorMessage(MonitorMessage other) : this() {
      Header = other.header_ != null ? other.Header.Clone() : null;
      item_ = other.item_.Clone();
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MonitorMessage Clone() {
      return new MonitorMessage(this);
    }

    /// <summary>Field number for the "header" field.</summary>
    public const int HeaderFieldNumber = 1;
    private global::Apollo.Common.Header header_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Apollo.Common.Header Header {
      get { return header_; }
      set {
        header_ = value;
      }
    }

    /// <summary>Field number for the "item" field.</summary>
    public const int ItemFieldNumber = 2;
    private static readonly pb::FieldCodec<global::Apollo.Common.Monitor.MonitorMessageItem> _repeated_item_codec
        = pb::FieldCodec.ForMessage(18, global::Apollo.Common.Monitor.MonitorMessageItem.Parser);
    private readonly pbc::RepeatedField<global::Apollo.Common.Monitor.MonitorMessageItem> item_ = new pbc::RepeatedField<global::Apollo.Common.Monitor.MonitorMessageItem>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Apollo.Common.Monitor.MonitorMessageItem> Item {
      get { return item_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as MonitorMessage);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(MonitorMessage other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Header, other.Header)) return false;
      if(!item_.Equals(other.item_)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (header_ != null) hash ^= Header.GetHashCode();
      hash ^= item_.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (header_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Header);
      }
      item_.WriteTo(output, _repeated_item_codec);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (header_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Header);
      }
      size += item_.CalculateSize(_repeated_item_codec);
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(MonitorMessage other) {
      if (other == null) {
        return;
      }
      if (other.header_ != null) {
        if (header_ == null) {
          header_ = new global::Apollo.Common.Header();
        }
        Header.MergeFrom(other.Header);
      }
      item_.Add(other.item_);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            if (header_ == null) {
              header_ = new global::Apollo.Common.Header();
            }
            input.ReadMessage(header_);
            break;
          }
          case 18: {
            item_.AddEntriesFrom(input, _repeated_item_codec);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code