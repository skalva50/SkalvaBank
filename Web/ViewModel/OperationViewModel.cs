using Microsoft.AspNetCore.Mvc.Rendering;
using SkalvaBank.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkalvaBank.Web
{
    public class OperationViewModel
    {
        public IEnumerable<Operation> ListOperations { get; set; }
        public SelectList Categories;

        public int? IdCategorie{get;set;}

        private DateTime? _dateSelected;
        [DataType(DataType.Date)]
        public DateTime? DateSelected 
        { 
            get
            {
                if(_dateSelected == null)
                {
                    _dateSelected = DateTime.Now;
                }
                return _dateSelected;
            }
            set 
            {
                _dateSelected = value; 
            } 
        }

        public double? TotalDepenses { get; set; }
        public double? TotalRecettes { get; set; }       
    }
}