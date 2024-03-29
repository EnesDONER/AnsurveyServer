﻿using Core.Entities;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class AdUploadDto:IDto
    {
        public int OwnerUserId { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string ImageContainerName { get; set; }
        public string VideoContainerName { get; set; }
        public IFormFile VideoUri { get; set; }
        public IFormFile VideoImage { get; set; }

    }
}
