//-----------------------------------------------------------------------
// <copyright file="Unit.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve {
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Diagnostics.Contracts;
  using System.Linq;

  using FreeNet;
  using FreeNet.Data.Entity;

  using Eve.Data.Entities;

  //******************************************************************************
  /// <summary>
  /// Contains information about a unit used to format numeric values.
  /// </summary>
  public class Unit : BaseValue<UnitId, UnitId, UnitEntity, Unit> {

    #region Constructors/Finalizers
    //******************************************************************************
    /// <summary>
    /// Initializes a new instance of the Unit class.
    /// </summary>
    /// 
    /// <param name="entity">
    /// The data entity that forms the basis of the adapter.
    /// </param>
    protected internal Unit(UnitEntity entity) : base(entity) {
      Contract.Requires(entity != null, Resources.Messages.EntityAdapter_EntityCannotBeNull);
    }
    //******************************************************************************
    /// <summary>
    /// Establishes object invariants of the class.
    /// </summary>
    [ContractInvariantMethod]
    private void ObjectInvariant() {
    }
    #endregion
    #region Public Properties
    //******************************************************************************
    /// <summary>
    /// Gets the display name of the unit.
    /// </summary>
    /// 
    /// <value>
    /// The display name of the unit.
    /// </value>
    /// 
    /// <remarks>
    /// <para>
    /// This is the suffix used to format numeric values according to the unit.
    /// </para>
    /// </remarks>
    public string DisplayName {
      get {
        Contract.Ensures(!string.IsNullOrWhiteSpace(Contract.Result<string>()));

        return (string.IsNullOrWhiteSpace(Entity.DisplayName) ? Name : Entity.DisplayName);
      }
    }
    #endregion
    #region Public Methods
    //******************************************************************************
    /// <summary>
    /// Formats a numeric value according to the current unit.
    /// </summary>
    /// 
    /// <param name="value">
    /// The value to format.
    /// </param>
    /// 
    /// <returns>
    /// A string containing a formatted version of the specified value.
    /// </returns>
    public string FormatValue(double value) {
      return FormatValue(value, string.Empty);
    }
    //******************************************************************************
    /// <summary>
    /// Formats a numeric value according to the current unit.
    /// </summary>
    /// 
    /// <param name="value">
    /// The value to format.
    /// </param>
    /// 
    /// <param name="numericFormat">
    /// The format string used to format the numeric portion of the result.
    /// </param>
    /// 
    /// <returns>
    /// A string containing a formatted version of the specified value.
    /// </returns>
    public string FormatValue(double value, string numericFormat) {
      Contract.Ensures(Contract.Result<string>() != null);

      if (numericFormat == null) {
        numericFormat = string.Empty;
      }

      string result;

      // Some unit types aren't formatted numerically
      switch (Id) {
        case UnitId.AbsolutePercent:
          result = (value * 100.0D).ToString(numericFormat);
          result = ApplyDisplayName(result);
          break;

        case UnitId.AttributeId:
          result = Eve.General.DataSource.GetAttributeTypeById((AttributeId) (int) value).DisplayName;
          break;

        case UnitId.Boolean:
          result = ((double) value == (double) 0.0D) ? "False" : "True";
          break;

        case UnitId.GroupId:
          result = Eve.General.DataSource.GetGroupById((GroupId) (int) value).Name;
          break;

        case UnitId.InverseAbsolutePercent:
          result = ((1.0D - value) * 100.0D).ToString(numericFormat);
          result = ApplyDisplayName(result);
          break;

        case UnitId.InversedModifierPercent:
          result = ((1.0D - value) * 100.0D).ToString(numericFormat);
          result = ApplyDisplayName(result);
          break;

        case UnitId.ModifierPercent:
          result = ((value - 1.0D) * 100.0D).ToString(numericFormat);
          result = ApplyDisplayName(result);

          // Include the positive/negative sign only if the format didn't already include one
          if (result.Length > 0 && result[0] != '+' && result[0] != '-') {
            result = (((value - 1.0D) < 0.0D) ? "-" : "+") + result;
          }
          break;

        case UnitId.ModifierRelativePercent:
          // Is this right?
          result = value.ToString(numericFormat);
          result = ApplyDisplayName(result);
          break;

        case UnitId.Sex:
          if (value < 0.5D) {
            result = "Unknown";

          } else if (value < 1.5D) {
            result = "Male";

          } else if (value < 2.5D) {
            result = "Unisex";

          } else if (value < 3.5D) {
            result = "Female";

          } else {
            result = "Unknown";
          }
          break;

        case UnitId.Sizeclass:
          if (value < 0.5D) {
            result = "Unknown";

          } else if (value < 1.5D) {
            result = "Small";

          } else if (value < 2.5D) {
            result = "Medium";

          } else if (value < 3.5D) {
            result = "Large";

          } else if (value < 4.5D) {
            result = "X-Large";
          
          } else {
            result = "Unknown";
          }
          break;

        case UnitId.TypeId:
          result = Eve.General.DataSource.GetEveTypeById<EveType>((int) value).Name;
          break;

        default:
          result = value.ToString(numericFormat);
          result = ApplyDisplayName(result);
          break;
      }

      return result;
    }
    #endregion
    #region Private Methods
    //******************************************************************************
    private string ApplyDisplayName(string numericPortion) {
      Contract.Requires(numericPortion != null, Resources.Messages.Unit_NumericPortionCannotBeNull);

      string result = numericPortion;

      // Some unit types require special processing
      switch (Id) {
        case UnitId.AbsolutePercent:
        case UnitId.InverseAbsolutePercent:
        case UnitId.InversedModifierPercent:
        case UnitId.ModifierPercent:
        case UnitId.ModifierRelativePercent:
        case UnitId.Percentage:
        case UnitId.RealPercent:
          result += "%";
          break;

        case UnitId.Acceleration:
          result += " m/s²";
          break;

        case UnitId.AmountOfSubstanceConcentration:
          result += " mol/m³";
          break;

        case UnitId.Area:
          result += " m²";
          break;

        case UnitId.AttributeId:
          break;

        case UnitId.Bonus:
          result = "+" + result;
          break;

        case UnitId.Boolean:
          break;

        case UnitId.CurrentDensity:
          result += " A/m²";
          break;

        case UnitId.DroneBandwidth:
          result += " Mbit/s";
          break;

        case UnitId.GroupId:
          break;

        case UnitId.Hours:
          result += " hours";
          break;

        case UnitId.Level:
          result = "Level " + numericPortion;
          break;

        case UnitId.LogisticalCapacity:
          result += " m³/hour";
          break;

        case UnitId.Luminance:
          result += " cd/m²";
          break;

        case UnitId.MassDensity:
          result += " kg/m³";
          break;

        case UnitId.MassFraction:
          break;

        case UnitId.MegaPascals:
          result += " MPa";
          break;

        case UnitId.Milliseconds:
          result += " ms";
          break;

        case UnitId.Multiplier:
          result += "×";
          break;

        case UnitId.RadiansSecond:
          result += " rad/sec";
          break;

        case UnitId.Sex:
          break;

        case UnitId.Sizeclass:
          break;

        case UnitId.Slot:
          result = "Slot " + numericPortion;
          break;

        case UnitId.SpecificVolume:
          result += " m³/kg";
          break;

        case UnitId.Speed:
          result += " m/s";
          break;

        case UnitId.TrueTime:
          result += " s";
          break;

        case UnitId.TypeId:
          break;

        case UnitId.Volume:
          result += " m³";
          break;

        case UnitId.WaveNumber:
          result += " m⁻¹";
          break;

        default:
          result += " " + DisplayName;
          break;
      }

      return result;
    }
    #endregion
  }
}