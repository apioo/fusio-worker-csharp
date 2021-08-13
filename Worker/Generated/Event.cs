/**
 * Autogenerated by Thrift Compiler (0.14.2)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Thrift;
using Thrift.Collections;

using Thrift.Protocol;
using Thrift.Protocol.Entities;
using Thrift.Protocol.Utilities;
using Thrift.Transport;
using Thrift.Transport.Client;
using Thrift.Transport.Server;
using Thrift.Processor;


#pragma warning disable IDE0079  // remove unnecessary pragmas
#pragma warning disable IDE1006  // parts of the code use IDL spelling


public partial class @Event : TBase
{
  private string _eventName;
  private string _data;

  public string EventName
  {
    get
    {
      return _eventName;
    }
    set
    {
      __isset.eventName = true;
      this._eventName = value;
    }
  }

  public string Data
  {
    get
    {
      return _data;
    }
    set
    {
      __isset.data = true;
      this._data = value;
    }
  }


  public Isset __isset;
  public struct Isset
  {
    public bool eventName;
    public bool data;
  }

  public @Event()
  {
  }

  public @Event DeepCopy()
  {
    var tmp65 = new @Event();
    if((EventName != null) && __isset.eventName)
    {
      tmp65.EventName = this.EventName;
    }
    tmp65.__isset.eventName = this.__isset.eventName;
    if((Data != null) && __isset.data)
    {
      tmp65.Data = this.Data;
    }
    tmp65.__isset.data = this.__isset.data;
    return tmp65;
  }

  public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      TField field;
      await iprot.ReadStructBeginAsync(cancellationToken);
      while (true)
      {
        field = await iprot.ReadFieldBeginAsync(cancellationToken);
        if (field.Type == TType.Stop)
        {
          break;
        }

        switch (field.ID)
        {
          case 1:
            if (field.Type == TType.String)
            {
              EventName = await iprot.ReadStringAsync(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 2:
            if (field.Type == TType.String)
            {
              Data = await iprot.ReadStringAsync(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          default: 
            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            break;
        }

        await iprot.ReadFieldEndAsync(cancellationToken);
      }

      await iprot.ReadStructEndAsync(cancellationToken);
    }
    finally
    {
      iprot.DecrementRecursionDepth();
    }
  }

  public async global::System.Threading.Tasks.Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
  {
    oprot.IncrementRecursionDepth();
    try
    {
      var struc = new TStruct("Event");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      if((EventName != null) && __isset.eventName)
      {
        field.Name = "eventName";
        field.Type = TType.String;
        field.ID = 1;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteStringAsync(EventName, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((Data != null) && __isset.data)
      {
        field.Name = "data";
        field.Type = TType.String;
        field.ID = 2;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteStringAsync(Data, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      await oprot.WriteFieldStopAsync(cancellationToken);
      await oprot.WriteStructEndAsync(cancellationToken);
    }
    finally
    {
      oprot.DecrementRecursionDepth();
    }
  }

  public override bool Equals(object that)
  {
    if (!(that is @Event other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return ((__isset.eventName == other.__isset.eventName) && ((!__isset.eventName) || (System.Object.Equals(EventName, other.EventName))))
      && ((__isset.data == other.__isset.data) && ((!__isset.data) || (System.Object.Equals(Data, other.Data))));
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      if((EventName != null) && __isset.eventName)
      {
        hashcode = (hashcode * 397) + EventName.GetHashCode();
      }
      if((Data != null) && __isset.data)
      {
        hashcode = (hashcode * 397) + Data.GetHashCode();
      }
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("Event(");
    int tmp66 = 0;
    if((EventName != null) && __isset.eventName)
    {
      if(0 < tmp66++) { sb.Append(", "); }
      sb.Append("EventName: ");
      EventName.ToString(sb);
    }
    if((Data != null) && __isset.data)
    {
      if(0 < tmp66++) { sb.Append(", "); }
      sb.Append("Data: ");
      Data.ToString(sb);
    }
    sb.Append(')');
    return sb.ToString();
  }
}
