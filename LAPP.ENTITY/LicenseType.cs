using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAPP.ENTITY
{
public class LicenseType:BaseEntity

{
public int LicenseTypeId { get; set; } 
public string LicenseTypeCode { get; set; } 
public string LicenseTypeName { get; set; } 
public bool IsActive { get; set; } 
public bool IsDeleted { get; set; } 
public int CreatedBy { get; set; } 
public DateTime CreatedOn { get; set; } 
public int ModifiedBy { get; set; } 
public DateTime ModifiedOn { get; set; } 
}
}
