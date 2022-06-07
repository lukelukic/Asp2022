﻿using ASPNedelja3.Application.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNedelja3.Application.UseCases.Commands
{
    public interface ICreateCategoryCommand : ICommand<CreateCategoryDto>
    {
    }
}
