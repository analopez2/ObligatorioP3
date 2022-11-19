﻿using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class UsuarioDTOViewModel
    {
       public DTOUsuario usuario { get; set; }
        public IEnumerable<string> UsuarioRol { get; set; }
    }
}
