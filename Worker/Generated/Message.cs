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

namespace FusioWorker.Generated
{

  public partial class Message : TBase
  {
    private bool _success;
    private string _message;

    public bool Success
    {
      get
      {
        return _success;
      }
      set
      {
        __isset.success = true;
        this._success = value;
      }
    }

    public string Message_
    {
      get
      {
        return _message;
      }
      set
      {
        __isset.message = true;
        this._message = value;
      }
    }


    public Isset __isset;
    public struct Isset
    {
      public bool success;
      public bool message;
    }

    public Message()
    {
    }

    public Message DeepCopy()
    {
      var tmp0 = new Message();
      if(__isset.success)
      {
        tmp0.Success = this.Success;
      }
      tmp0.__isset.success = this.__isset.success;
      if((Message_ != null) && __isset.message)
      {
        tmp0.Message_ = this.Message_;
      }
      tmp0.__isset.message = this.__isset.message;
      return tmp0;
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
              if (field.Type == TType.Bool)
              {
                Success = await iprot.ReadBoolAsync(cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 2:
              if (field.Type == TType.String)
              {
                Message_ = await iprot.ReadStringAsync(cancellationToken);
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
        var struc = new TStruct("Message");
        await oprot.WriteStructBeginAsync(struc, cancellationToken);
        var field = new TField();
        if(__isset.success)
        {
          field.Name = "success";
          field.Type = TType.Bool;
          field.ID = 1;
          await oprot.WriteFieldBeginAsync(field, cancellationToken);
          await oprot.WriteBoolAsync(Success, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        if((Message_ != null) && __isset.message)
        {
          field.Name = "message";
          field.Type = TType.String;
          field.ID = 2;
          await oprot.WriteFieldBeginAsync(field, cancellationToken);
          await oprot.WriteStringAsync(Message_, cancellationToken);
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
      if (!(that is Message other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return ((__isset.success == other.__isset.success) && ((!__isset.success) || (System.Object.Equals(Success, other.Success))))
        && ((__isset.message == other.__isset.message) && ((!__isset.message) || (System.Object.Equals(Message_, other.Message_))));
    }

    public override int GetHashCode() {
      int hashcode = 157;
      unchecked {
        if(__isset.success)
        {
          hashcode = (hashcode * 397) + Success.GetHashCode();
        }
        if((Message_ != null) && __isset.message)
        {
          hashcode = (hashcode * 397) + Message_.GetHashCode();
        }
      }
      return hashcode;
    }

    public override string ToString()
    {
      var sb = new StringBuilder("Message(");
      int tmp1 = 0;
      if(__isset.success)
      {
        if(0 < tmp1++) { sb.Append(", "); }
        sb.Append("Success: ");
        Success.ToString(sb);
      }
      if((Message_ != null) && __isset.message)
      {
        if(0 < tmp1++) { sb.Append(", "); }
        sb.Append("Message_: ");
        Message_.ToString(sb);
      }
      sb.Append(')');
      return sb.ToString();
    }
  }

}
