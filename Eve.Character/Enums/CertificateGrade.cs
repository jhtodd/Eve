//-----------------------------------------------------------------------
// <copyright file="CertificateGrade.cs" company="Jeremy H. Todd">
//     Copyright © Jeremy H. Todd 2011
// </copyright>
//-----------------------------------------------------------------------
namespace Eve.Character
{
  using System;
  using System.ComponentModel;

  using FreeNet;

  /// <summary>
  /// The grade of a skill certification.
  /// </summary>
  public enum CertificateGrade : byte
  {
    /// <summary>
    /// The certificate represents a basic level of competence.
    /// </summary>
    [Description("Basic")]
    [LongDescription("The certificate represents a basic level of competence.")]
    Basic = 1,

    /// <summary>
    /// The certificate represents a standard level of competence.
    /// </summary>
    [Description("Standard")]
    [LongDescription("The certificate represents a standard level of competence.")]
    Standard = 2,

    /// <summary>
    /// The certificate represents an improved level of competence.
    /// </summary>
    [Description("Improved")]
    [LongDescription("The certificate represents an improved level of competence.")]
    Improved = 3,

    /// <summary>
    /// The certificate represents an elite level of competence.
    /// </summary>
    [Description("Elite")]
    [LongDescription("The certificate represents an elite level of competence.")]
    Elite = 5,
  }
}