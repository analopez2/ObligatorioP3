﻿using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excepciones;

namespace LogicaAccesoDatos.BaseDatos
{
     public class RepositorioSelecciones:IRepositorioSelecciones
    {
        public LibreriaContext Contexto { get; set; }

        public RepositorioSelecciones(LibreriaContext ctx)
        {
            Contexto = ctx;
        }

        public int CalcularPuntos(List<Partido> partidos)
        {
            throw new NotImplementedException();
        }

        public void Add(Seleccion obj)
        {
            try
            {
                obj.Validar();
                Contexto.Selecciones.Add(obj);
                Contexto.SaveChanges();
            }
            catch (SeleccionException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo dar de alta la Seleccion", e);
            }
        }

        public void Remove(int Id)
        {
            try
            {
                Seleccion seleccionABorrar = FindById(Id);
                if (seleccionABorrar == null) throw new SeleccionException("No existe la seleccion a borrar");

                var partidos = Contexto.SeleccionPartido.Where(sp => sp.SeleccionId == Id);
                bool hayPartidosSeleccion = partidos.Count() > 0;

                if(hayPartidosSeleccion) throw new SeleccionException("No se puede borrar la seleccion porque existen registros asociados a ella");


                Contexto.Selecciones.Remove(seleccionABorrar);
                Contexto.SaveChanges();
            }
            catch(SeleccionException e)
            {
                return;
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo borrar la seleccion", e);
            }
        }

        public void Update(Seleccion obj)
        {
            try
            {
                obj.Validar();
                Contexto.Selecciones.Update(obj);
                Contexto.SaveChanges();
            }
            catch (SeleccionException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo actualizar la seleccion", e);
            }
        }

        public Seleccion FindById(int Id)
        {
            try
            {
                if (Id == 0) throw new SeleccionException("El id de seleccion no puede ser 0");
                return Contexto.Selecciones
                    .Include(s => s.Pais)
                    .Include(s => s.SeleccionPartido)
                    .Include(s=>s.Grupo)
                    .Where(s => s.Id == Id)
                    .SingleOrDefault();
            }
            catch (SeleccionException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo encontrar la seleccion", e);
            }
        }

        public IEnumerable<Seleccion> FindAll()
        {
            try
            {
                return Contexto.Selecciones
                    .Include(s => s.Pais)
                    .Include(s => s.SeleccionPartido)
                    .Include(s => s.Grupo)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception("No se pudieron encontrar selecciones", e);
            }
        }
    }
}
