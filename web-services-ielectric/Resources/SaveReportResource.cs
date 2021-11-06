﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web_services_ielectric.Resources
{
    public class SaveReportResource
    {
        [Required]
        [MaxLength(30)]
        public string Observation { get; set; }

        [Required]
        [MaxLength(30)]
        public string Diagnosis { get; set; }

        [Required]
        [MaxLength(200)]
        public string RepairDescription { get; set; }

        [Required]
        [MaxLength(10)]
        public string Date { get; set; }

        [Required]
        [MaxLength(100)]
        public string ImagePath { get; set; }

        [Required]
        [MaxLength(30)]
        public long AppointmentId { get; set; }
        public long TechnicianId { get; set; }
    }
}
