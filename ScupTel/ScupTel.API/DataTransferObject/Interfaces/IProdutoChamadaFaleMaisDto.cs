﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScupTel.API.DataTransferObject.Interfaces
{
    public interface IProdutoChamadaFaleMaisDto : IProdutoDto
    {
        int MinutosFranquia { get; set; }
    }
}
