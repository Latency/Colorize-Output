﻿// ****************************************************************************
// * Project:  Colorize-Output
// * File:     IRegistryKey.cs
// * Date:     07/26/2014
// ****************************************************************************

#region

using System;
using Microsoft.Win32;

#endregion

namespace ColorizeOutput {
  public interface IRegistryKey : IDisposable {
    object GetValue(string name);
    void SetValue(string name, object value);
  }

  public class RegistryKeyImpl : IRegistryKey {
    private RegistryKey _registryKey;

    public RegistryKeyImpl(RegistryKey registryKey) {
      _registryKey = registryKey;
    }

    public object GetValue(string name) {
      return _registryKey.GetValue(name);
    }

    public void SetValue(string name, object value) {
      _registryKey.SetValue(name, value);
    }

    public void Dispose() {
      if (_registryKey != null) {
        _registryKey.Close();
        _registryKey = null;
      }
    }
  }
}