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


public partial class Context : TBase
{
  private long _routeId;
  private string _baseUrl;
  private App _app;
  private User _user;

  public long RouteId
  {
    get
    {
      return _routeId;
    }
    set
    {
      __isset.routeId = true;
      this._routeId = value;
    }
  }

  public string BaseUrl
  {
    get
    {
      return _baseUrl;
    }
    set
    {
      __isset.baseUrl = true;
      this._baseUrl = value;
    }
  }

  public App App
  {
    get
    {
      return _app;
    }
    set
    {
      __isset.app = true;
      this._app = value;
    }
  }

  public User User
  {
    get
    {
      return _user;
    }
    set
    {
      __isset.user = true;
      this._user = value;
    }
  }


  public Isset __isset;
  public struct Isset
  {
    public bool routeId;
    public bool baseUrl;
    public bool app;
    public bool user;
  }

  public Context()
  {
  }

  public Context DeepCopy()
  {
    var tmp34 = new Context();
    if(__isset.routeId)
    {
      tmp34.RouteId = this.RouteId;
    }
    tmp34.__isset.routeId = this.__isset.routeId;
    if((BaseUrl != null) && __isset.baseUrl)
    {
      tmp34.BaseUrl = this.BaseUrl;
    }
    tmp34.__isset.baseUrl = this.__isset.baseUrl;
    if((App != null) && __isset.app)
    {
      tmp34.App = (App)this.App.DeepCopy();
    }
    tmp34.__isset.app = this.__isset.app;
    if((User != null) && __isset.user)
    {
      tmp34.User = (User)this.User.DeepCopy();
    }
    tmp34.__isset.user = this.__isset.user;
    return tmp34;
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
            if (field.Type == TType.I64)
            {
              RouteId = await iprot.ReadI64Async(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 2:
            if (field.Type == TType.String)
            {
              BaseUrl = await iprot.ReadStringAsync(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 3:
            if (field.Type == TType.Struct)
            {
              App = new App();
              await App.ReadAsync(iprot, cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 4:
            if (field.Type == TType.Struct)
            {
              User = new User();
              await User.ReadAsync(iprot, cancellationToken);
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
      var struc = new TStruct("Context");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      if(__isset.routeId)
      {
        field.Name = "routeId";
        field.Type = TType.I64;
        field.ID = 1;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteI64Async(RouteId, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((BaseUrl != null) && __isset.baseUrl)
      {
        field.Name = "baseUrl";
        field.Type = TType.String;
        field.ID = 2;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteStringAsync(BaseUrl, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((App != null) && __isset.app)
      {
        field.Name = "app";
        field.Type = TType.Struct;
        field.ID = 3;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await App.WriteAsync(oprot, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((User != null) && __isset.user)
      {
        field.Name = "user";
        field.Type = TType.Struct;
        field.ID = 4;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await User.WriteAsync(oprot, cancellationToken);
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
    if (!(that is Context other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return ((__isset.routeId == other.__isset.routeId) && ((!__isset.routeId) || (System.Object.Equals(RouteId, other.RouteId))))
      && ((__isset.baseUrl == other.__isset.baseUrl) && ((!__isset.baseUrl) || (System.Object.Equals(BaseUrl, other.BaseUrl))))
      && ((__isset.app == other.__isset.app) && ((!__isset.app) || (System.Object.Equals(App, other.App))))
      && ((__isset.user == other.__isset.user) && ((!__isset.user) || (System.Object.Equals(User, other.User))));
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      if(__isset.routeId)
      {
        hashcode = (hashcode * 397) + RouteId.GetHashCode();
      }
      if((BaseUrl != null) && __isset.baseUrl)
      {
        hashcode = (hashcode * 397) + BaseUrl.GetHashCode();
      }
      if((App != null) && __isset.app)
      {
        hashcode = (hashcode * 397) + App.GetHashCode();
      }
      if((User != null) && __isset.user)
      {
        hashcode = (hashcode * 397) + User.GetHashCode();
      }
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("Context(");
    int tmp35 = 0;
    if(__isset.routeId)
    {
      if(0 < tmp35++) { sb.Append(", "); }
      sb.Append("RouteId: ");
      RouteId.ToString(sb);
    }
    if((BaseUrl != null) && __isset.baseUrl)
    {
      if(0 < tmp35++) { sb.Append(", "); }
      sb.Append("BaseUrl: ");
      BaseUrl.ToString(sb);
    }
    if((App != null) && __isset.app)
    {
      if(0 < tmp35++) { sb.Append(", "); }
      sb.Append("App: ");
      App.ToString(sb);
    }
    if((User != null) && __isset.user)
    {
      if(0 < tmp35++) { sb.Append(", "); }
      sb.Append("User: ");
      User.ToString(sb);
    }
    sb.Append(')');
    return sb.ToString();
  }
}

