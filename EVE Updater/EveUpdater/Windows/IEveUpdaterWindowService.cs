//-----------------------------------------------------------------------
// <copyright file="IEveUpdaterWindowService.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2012
// </copyright>
//-----------------------------------------------------------------------
namespace EveUpdater {
  using System;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using System.Windows;
  using System.Windows.Data;

  using FreeNet;
  using FreeNet.Windows;

  //******************************************************************************
  /// <summary>
  /// The base interface for services related to the
  /// <see cref="EveUpdaterWindow" /> class.
  /// </summary>
  public interface IEveUpdaterWindowService : IViewModelService<EveUpdaterWindowViewModel, IEveUpdaterWindowService> {
  }

  //******************************************************************************
  /// <summary>
  /// The default implementation of the <see cref="IEveUpdaterWindowService" />
  /// service.
  /// </summary>
  public class DefaultEveUpdaterWindowService : ViewModelServiceBase<EveUpdaterWindowViewModel, IEveUpdaterWindowService>,
                                                 IEveUpdaterWindowService {
  }
}