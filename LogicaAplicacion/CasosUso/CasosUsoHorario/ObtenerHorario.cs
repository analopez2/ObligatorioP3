﻿using LogicaAplicacion.InterfacesCasosUso.ICasosUsoHorario;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicaAplicacion.CasosUso.CasosUsoHorario
{
    public class ObtenerHorario : IObtenerHorario
    {
        public IRepositorioHorarios RepoHorarios { get; set; }

        public ObtenerHorario(IRepositorioHorarios repoHorarios)
        {
            RepoHorarios = repoHorarios;
        }
        public void FindById(int id)
        {
            try
            {
                RepoHorarios.FindById(id);
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo obter el horario con el id indicado", e);
            }
        }
    }
}
