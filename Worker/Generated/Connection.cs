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


public partial class Connection : TBase
{
  private string _name;
  private string _type;
  private Dictionary<string, string> _config;

  public string Name
  {
    get
    {
      return _name;
    }
    set
    {
      __isset.name = true;
      this._name = value;
    }
  }

  public string Type
  {
    get
    {
      return _type;
    }
    set
    {
      __isset.type = true;
      this._type = value;
    }
  }

  public Dictionary<string, string> Config
  {
    get
    {
      return _config;
    }
    set
    {
      __isset.config = true;
      this._config = value;
    }
  }


  public Isset __isset;
  public struct Isset
  {
    public bool name;
    public bool type;
    public bool config;
  }

  public Connection()
  {
  }

  public Connection DeepCopy()
  {
    var tmp2 = new Connection();
    if((Name != null) && __isset.name)
    {
      tmp2.Name = this.Name;
    }
    tmp2.__isset.name = this.__isset.name;
    if((Type != null) && __isset.type)
    {
      tmp2.Type = this.Type;
    }
    tmp2.__isset.type = this.__isset.type;
    if((Config != null) && __isset.config)
    {
      tmp2.Config = this.Config.DeepCopy();
    }
    tmp2.__isset.config = this.__isset.config;
    return tmp2;
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
              Name = await iprot.ReadStringAsync(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 2:
            if (field.Type == TType.String)
            {
              Type = await iprot.ReadStringAsync(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 3:
            if (field.Type == TType.Map)
            {
              {
                TMap _map3 = await iprot.ReadMapBeginAsync(cancellationToken);
                Config = new Dictionary<string, string>(_map3.Count);
                for(int _i4 = 0; _i4 < _map3.Count; ++_i4)
                {
                  string _key5;
                  string _val6;
                  _key5 = await iprot.ReadStringAsync(cancellationToken);
                  _val6 = await iprot.ReadStringAsync(cancellationToken);
                  Config[_key5] = _val6;
                }
                await iprot.ReadMapEndAsync(cancellationToken);
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
      var struc = new TStruct("Connection");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      if((Name != null) && __isset.name)
      {
        field.Name = "name";
        field.Type = TType.String;
        field.ID = 1;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteStringAsync(Name, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((Type != null) && __isset.type)
      {
        field.Name = "type";
        field.Type = TType.String;
        field.ID = 2;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteStringAsync(Type, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((Config != null) && __isset.config)
      {
        field.Name = "config";
        field.Type = TType.Map;
        field.ID = 3;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        {
          await oprot.WriteMapBeginAsync(new TMap(TType.String, TType.String, Config.Count), cancellationToken);
          foreach (string _iter7 in Config.Keys)
          {
            await oprot.WriteStringAsync(_iter7, cancellationToken);
            await oprot.WriteStringAsync(Config[_iter7], cancellationToken);
          }
          await oprot.WriteMapEndAsync(cancellationToken);
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
    if (!(that is Connection other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return ((__isset.name == other.__isset.name) && ((!__isset.name) || (System.Object.Equals(Name, other.Name))))
      && ((__isset.type == other.__isset.type) && ((!__isset.type) || (System.Object.Equals(Type, other.Type))))
      && ((__isset.config == other.__isset.config) && ((!__isset.config) || (TCollections.Equals(Config, other.Config))));
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      if((Name != null) && __isset.name)
      {
        hashcode = (hashcode * 397) + Name.GetHashCode();
      }
      if((Type != null) && __isset.type)
      {
        hashcode = (hashcode * 397) + Type.GetHashCode();
      }
      if((Config != null) && __isset.config)
      {
        hashcode = (hashcode * 397) + TCollections.GetHashCode(Config);
      }
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("Connection(");
    int tmp8 = 0;
    if((Name != null) && __isset.name)
    {
      if(0 < tmp8++) { sb.Append(", "); }
      sb.Append("Name: ");
      Name.ToString(sb);
    }
    if((Type != null) && __isset.type)
    {
      if(0 < tmp8++) { sb.Append(", "); }
      sb.Append("Type: ");
      Type.ToString(sb);
    }
    if((Config != null) && __isset.config)
    {
      if(0 < tmp8++) { sb.Append(", "); }
      sb.Append("Config: ");
      Config.ToString(sb);
    }
    sb.Append(')');
    return sb.ToString();
  }
}

