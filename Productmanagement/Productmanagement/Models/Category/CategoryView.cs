﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Productmanagement.Models.Category
{
    public class CategoryView
    {
        public List<Entities.Category> Categories { get; set; }
        public Pagination Pagination { get; set; }
    }
}
