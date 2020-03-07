using Microsoft.EntityFrameworkCore;
using RegistroDetallado.DAL;
using RegistroDetallado.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace RegistroDetallado.BLL
{
    public class PersonasBLL
    {
        public static bool Guardar(Personas persona)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Personas.Add(persona) != null)
                    paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }

        public static bool Modificar(Personas persona)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var Anterior = PersonasBLL.Buscar(persona.PersonaId);
                //var Anterior = db.Personas.Find(persona.PersonaId);

                foreach(var item in Anterior.Telefonos)
                {
                    if (!persona.Telefonos.Exists(d => d.Id == item.Id))
                        db.Entry(item).State = EntityState.Deleted;
                }

                foreach (var item in persona.Telefonos)
                {
                    var estado = item.Id > 0 ? EntityState.Modified : EntityState.Added;
                        db.Entry(item).State = estado;
                }

                db.Entry(persona).State = EntityState.Modified;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Eliminar(int Id)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var Eliminar = PersonasBLL.Buscar(Id);
                db.Entry(Eliminar).State = EntityState.Deleted;
                paso = (db.SaveChanges() > 0);

                /*
                db.Personas.ExecuteSqlRaw($"Delete from TelefonosDetalle" +
                                          $"Where Id_Detalle = {entity.Id_Detalle}");

                foreach(var Telefonos in entity.TelefonosDetalle)
                {
                    db.Entry(Telefonos).State = EntityState.Added;
                }
                */
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static Personas Buscar(int Id)
        {
            Personas p = new Personas();
            Contexto db = new Contexto();

            try
            {
              //  p = db.Personas.Find(Id);
                //p.Telefonos.Count();

                p = db.Personas.Include(x => x.Telefonos)
                    .Where(x => x.PersonaId == Id)
                    .SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return p;
        }

        public static List<Personas> GetList(Expression<Func<Personas, bool>> persona)
        {
            List<Personas> Lista = new List<Personas>();
            Contexto db = new Contexto();

            try
            {
                Lista = db.Personas.Where(persona).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return Lista;
        }
        
    }
}
