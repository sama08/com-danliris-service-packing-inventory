﻿using Com.Danliris.Service.Packing.Inventory.Application.CommonViewModelObjectProperties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Packing.Inventory.Application.InventoryDocumentPacking
{
    public class IndexViewModel
    {
        public string Code { get; set; }
        public DateTimeOffset Date { get; set; }
        public int Id { get; set; }
        public string ReferenceNo { get; set; }
        public string ReferenceType { get; set; }
        public string Remark { get; set; }
        public Storage Storage { get; set; }
        public string Type { get; set; }
    }
}
