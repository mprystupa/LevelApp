﻿using System;
using System.Collections.Generic;

namespace LevelApp.DAL.Entities
{
    public partial class Schemaversions
    {
        public int Schemaversionid { get; set; }
        public string Scriptname { get; set; }
        public DateTime Applied { get; set; }
    }
}
