using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace orchard.cms.web.Models.Migration
{
    public class LegacyDocument
    {
        public int DocumentId { get; set; }
        public string Title { get; set; }
        public string OriginalFilename { get; set; }
        public int? Origination { get; set; }
        public string Description { get; set; }
        public string DirectURL { get; set; }
        public string DocNumber { get; set; }
        public string DocumentType { get; set; }
        public string FileTypes { get; set; }
        public string HyperlinkUrl { get; set; }
        public string InternalDescription { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime? PostDate { get; set; }
        public string ProductFamily { get; set; }
        public string ProductImage { get; set; }
        public string RelativePath { get; set; }
        public int? UploadedByPersonId { get; set; }
        public string UploadPathCategory { get; set; }
        public string Version { get; set; }
    }
}
