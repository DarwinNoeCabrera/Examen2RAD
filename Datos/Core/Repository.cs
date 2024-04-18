﻿using Datos.BaseDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Core
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public Connection dbcontext;

        public Repository()
        {
            this.dbcontext = new Connection();
        }
        public void Agregar(T entidad)
        {
            dbcontext.Set<T>().Add(entidad);
      
        }

        public void AgregarRango(IEnumerable<T> entidades)
        {
            dbcontext.Set<T>().AddRange(entidades);
         
        }

        public void Buscar(T entidad)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Consulta()
        {
            return dbcontext.Set<T>().AsQueryable();
        }

        public void Editar(T entidad)
        {
            dbcontext.Set<T>();
          
        }

        public void Eliminar(T entidad)
        {
            dbcontext.Set<T>().Remove(entidad);
            
        }
        public T ObtenerPorId(int id)
        {
            return dbcontext.Set<T>().Find(id);
        }
    }
}
