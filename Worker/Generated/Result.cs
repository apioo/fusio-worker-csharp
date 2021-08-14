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

  public partial class Result : TBase
  {
    private global::FusioWorker.Generated.Response _response;
    private List<global::FusioWorker.Generated.@Event> _events;
    private List<global::FusioWorker.Generated.Log> _logs;

    public global::FusioWorker.Generated.Response Response
    {
      get
      {
        return _response;
      }
      set
      {
        __isset.response = true;
        this._response = value;
      }
    }

    public List<global::FusioWorker.Generated.@Event> Events
    {
      get
      {
        return _events;
      }
      set
      {
        __isset.events = true;
        this._events = value;
      }
    }

    public List<global::FusioWorker.Generated.Log> Logs
    {
      get
      {
        return _logs;
      }
      set
      {
        __isset.logs = true;
        this._logs = value;
      }
    }


    public Isset __isset;
    public struct Isset
    {
      public bool response;
      public bool events;
      public bool logs;
    }

    public Result()
    {
    }

    public Result DeepCopy()
    {
      var tmp48 = new Result();
      if((Response != null) && __isset.response)
      {
        tmp48.Response = (global::FusioWorker.Generated.Response)this.Response.DeepCopy();
      }
      tmp48.__isset.response = this.__isset.response;
      if((Events != null) && __isset.events)
      {
        tmp48.Events = this.Events.DeepCopy();
      }
      tmp48.__isset.events = this.__isset.events;
      if((Logs != null) && __isset.logs)
      {
        tmp48.Logs = this.Logs.DeepCopy();
      }
      tmp48.__isset.logs = this.__isset.logs;
      return tmp48;
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
              if (field.Type == TType.Struct)
              {
                Response = new global::FusioWorker.Generated.Response();
                await Response.ReadAsync(iprot, cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 2:
              if (field.Type == TType.List)
              {
                {
                  TList _list49 = await iprot.ReadListBeginAsync(cancellationToken);
                  Events = new List<global::FusioWorker.Generated.@Event>(_list49.Count);
                  for(int _i50 = 0; _i50 < _list49.Count; ++_i50)
                  {
                    global::FusioWorker.Generated.@Event _elem51;
                    _elem51 = new global::FusioWorker.Generated.@Event();
                    await _elem51.ReadAsync(iprot, cancellationToken);
                    Events.Add(_elem51);
                  }
                  await iprot.ReadListEndAsync(cancellationToken);
                }
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 3:
              if (field.Type == TType.List)
              {
                {
                  TList _list52 = await iprot.ReadListBeginAsync(cancellationToken);
                  Logs = new List<global::FusioWorker.Generated.Log>(_list52.Count);
                  for(int _i53 = 0; _i53 < _list52.Count; ++_i53)
                  {
                    global::FusioWorker.Generated.Log _elem54;
                    _elem54 = new global::FusioWorker.Generated.Log();
                    await _elem54.ReadAsync(iprot, cancellationToken);
                    Logs.Add(_elem54);
                  }
                  await iprot.ReadListEndAsync(cancellationToken);
                }
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
        var struc = new TStruct("Result");
        await oprot.WriteStructBeginAsync(struc, cancellationToken);
        var field = new TField();
        if((Response != null) && __isset.response)
        {
          field.Name = "response";
          field.Type = TType.Struct;
          field.ID = 1;
          await oprot.WriteFieldBeginAsync(field, cancellationToken);
          await Response.WriteAsync(oprot, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        if((Events != null) && __isset.events)
        {
          field.Name = "events";
          field.Type = TType.List;
          field.ID = 2;
          await oprot.WriteFieldBeginAsync(field, cancellationToken);
          {
            await oprot.WriteListBeginAsync(new TList(TType.Struct, Events.Count), cancellationToken);
            foreach (global::FusioWorker.Generated.@Event _iter55 in Events)
            {
              await _iter55.WriteAsync(oprot, cancellationToken);
            }
            await oprot.WriteListEndAsync(cancellationToken);
          }
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        if((Logs != null) && __isset.logs)
        {
          field.Name = "logs";
          field.Type = TType.List;
          field.ID = 3;
          await oprot.WriteFieldBeginAsync(field, cancellationToken);
          {
            await oprot.WriteListBeginAsync(new TList(TType.Struct, Logs.Count), cancellationToken);
            foreach (global::FusioWorker.Generated.Log _iter56 in Logs)
            {
              await _iter56.WriteAsync(oprot, cancellationToken);
            }
            await oprot.WriteListEndAsync(cancellationToken);
          }
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
      if (!(that is Result other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return ((__isset.response == other.__isset.response) && ((!__isset.response) || (System.Object.Equals(Response, other.Response))))
        && ((__isset.events == other.__isset.events) && ((!__isset.events) || (TCollections.Equals(Events, other.Events))))
        && ((__isset.logs == other.__isset.logs) && ((!__isset.logs) || (TCollections.Equals(Logs, other.Logs))));
    }

    public override int GetHashCode() {
      int hashcode = 157;
      unchecked {
        if((Response != null) && __isset.response)
        {
          hashcode = (hashcode * 397) + Response.GetHashCode();
        }
        if((Events != null) && __isset.events)
        {
          hashcode = (hashcode * 397) + TCollections.GetHashCode(Events);
        }
        if((Logs != null) && __isset.logs)
        {
          hashcode = (hashcode * 397) + TCollections.GetHashCode(Logs);
        }
      }
      return hashcode;
    }

    public override string ToString()
    {
      var sb = new StringBuilder("Result(");
      int tmp57 = 0;
      if((Response != null) && __isset.response)
      {
        if(0 < tmp57++) { sb.Append(", "); }
        sb.Append("Response: ");
        Response.ToString(sb);
      }
      if((Events != null) && __isset.events)
      {
        if(0 < tmp57++) { sb.Append(", "); }
        sb.Append("Events: ");
        Events.ToString(sb);
      }
      if((Logs != null) && __isset.logs)
      {
        if(0 < tmp57++) { sb.Append(", "); }
        sb.Append("Logs: ");
        Logs.ToString(sb);
      }
      sb.Append(')');
      return sb.ToString();
    }
  }

}
