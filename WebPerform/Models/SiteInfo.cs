using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPerform.Models
{
    public class SiteInfo 
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        public string Site { get; set; }
        public string PageName { get; set; }
        public int SpeedMin { get; set; }
        public int SpeedMax { get; set; }
        public SiteInfo()
        {
            Site = "";
            PageName = "";
            SpeedMax = 0;
            SpeedMin = 0;
        }
        public SiteInfo(string site, string name, int min, int max)
        {
            this.Site = site;
            PageName = name;
            SpeedMax = max;
            SpeedMin = min;
        }
        public override string ToString()
        {
            return PageName + ',' + SpeedMax + ',' + SpeedMin;
        }

  
    }
}