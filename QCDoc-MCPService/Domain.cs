using System;

namespace QCDocMCPService;

    // Vendor model: represents a supplier or vendor in the system.
    public partial class Vendor
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ContactName { get; set; }
        public bool? IsActive { get; set; }
    }

    // Product model: each product is associated with a vendor.
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int VendorId { get; set; }        // Foreign key to Vendor
        public string Category { get; set; } = string.Empty;
        public string QualityStatus { get; set; } = "Pending"; // e.g. Pending, Approved, Rejected
    }

    // Lab Sample model: represents a sample of a product sent for testing.
    public class LabSample
    {
        public int Id { get; set; }
        public int ProductId { get; set; }      // Foreign key to Product
        public DateTime LoggedDate { get; set; }
        public string Status { get; set; } = "Pending"; // e.g. Pending, Completed
        public string? Result { get; set; } = null;     // e.g. Pass, Fail (null if not completed)
    }

    // VQCA Agreement model: represents a quality/compliance agreement with a vendor.
    public class QualityAgreement
    {
        public int Id { get; set; }
        public int VendorId { get; set; }       // Associated vendor
        public DateTime EffectiveDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Terms { get; set; } = string.Empty;
    }

    // Certificate of Analysis model: issued after lab testing is completed.
    public class CertificateOfAnalysis
    {
        public int Id { get; set; }
        public int SampleId { get; set; }       // Associated LabSample
        public DateTime IssuedDate { get; set; }
        public string Summary { get; set; } = string.Empty;  // Brief summary of results
    }

