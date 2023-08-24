﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Domain.Core.Shared;

namespace TaxCalculator.Domain.Core.Entities
{
    public class TaxType : IEntity
    {
        #region Constructors

        public TaxType(string code, string type) 
        {
            Code = code;
            Type = type;
        }

        #endregion

        #region Properties

        public int Id { get; private set; }
        public string Code { get; private set; }
        public string Type { get; private set; }

        #endregion

        #region Public Methods

        public static TaxType Create(string code, string type) 
        { 
            return new TaxType(code, type);
        }

        public void Update(string code, string type)
        {
            Code = code;
            Type = type;
        }

        #endregion

        #region Private Methods
        #endregion
    }
}