using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ProductoNegocio
    {
        public List<Producto> listar()
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();
            ImagenNegocio imagenNegocio = new ImagenNegocio();

            try
            {
                datos.setearConsulta(@"SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, M.Descripcion AS Marca, C.Descripcion AS Categoria FROM ARTICULOS A INNER JOIN MARCAS M ON A.IdMarca = M.Id INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto prod = new Producto
                    {
                        Id = (int)datos.Lector["Id"],
                        Codigo = (string)datos.Lector["Codigo"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Descripcion = (string)datos.Lector["Descripcion"],
                        Precio = (decimal)datos.Lector["Precio"],
                        Marca = new Marca { Descripcion = (string)datos.Lector["Marca"] },
                        Categoria = new Categoria { Descripcion = (string)datos.Lector["Categoria"] },
                        Imagenes = new List<Imagen>()
                    };

                    int idArticulo = (int)datos.Lector["Id"];
                    prod.Imagenes = imagenNegocio.listar(idArticulo);

                    lista.Add(prod);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }


        }

        public void agregar(Producto nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("BEGIN TRANSACTION; INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) VALUES (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio); DECLARE @IdArticulo INT = SCOPE_IDENTITY(); INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@IdArticulo, @ImagenUrl); COMMIT TRANSACTION;");
                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@ImagenUrl", nuevo.Imagenes[0].UrlImagen);
                datos.setearParametro("@IdMarca", nuevo.Marca.Id);
                datos.setearParametro("@IdCategoria", nuevo.Categoria.Id);
                datos.setearParametro("@Precio", nuevo.Precio);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public List<Producto> buscar(string Codigo, string Nombre, string Descripcion, string Marca, string Categoria, decimal? precio)
        {
            List<Producto> buscar = new List<Producto>();

            // para obtener los datos de la base de datos
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consultaBase = "SELECT A.id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, M.Descripcion AS Marca, C.Descripcion AS Categoria, I.ImagenUrl FROM ARTICULOS A LEFT JOIN MARCAS M ON A.IdMarca = M.Id LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id LEFT JOIN (SELECT IdArticulo, MIN(ImagenUrl) AS ImagenUrl FROM IMAGENES GROUP BY IdArticulo) I ON I.IdArticulo = A.id";
                string condiciones = "";

                if (!string.IsNullOrEmpty(Codigo))
                {
                    condiciones += " AND A.Codigo = @Codigo";
                    datos.setearParametro("@Codigo", Codigo);
                }
                if (!string.IsNullOrEmpty(Nombre))
                {
                    condiciones += " AND A.Nombre LIKE @Nombre";
                    datos.setearParametro("@Nombre", "%" + Nombre + "%");
                }
                if (!string.IsNullOrEmpty(Descripcion))
                {
                    condiciones += " AND A.Descripcion LIKE @Descripcion";
                    datos.setearParametro("@Descripcion", "%" + Descripcion + "%");
                }
                if (!string.IsNullOrEmpty(Marca))
                {
                    condiciones += " AND M.Descripcion LIKE @Marca";
                    datos.setearParametro("@Marca", "%" + Marca + "%");
                }
                if (!string.IsNullOrEmpty(Categoria))
                {
                    condiciones += " AND C.Descripcion LIKE @Categoria";
                    datos.setearParametro("@Categoria", "%" + Categoria + "%");
                }
                if (precio.HasValue)
                {
                    condiciones += " AND A.Precio = @Precio";
                    datos.setearParametro("@Precio", precio.Value);
                }

                if (!string.IsNullOrEmpty(condiciones))
                {
                    condiciones = " WHERE " + condiciones.Substring(5);
                }

                string consultaFinal = consultaBase + condiciones;
                datos.setearConsulta(consultaFinal);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.Id = (int)datos.Lector["id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Marca = new Marca();
                    aux.Marca.Descripcion = datos.Lector["Marca"] == DBNull.Value ? null : (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();

                    // Si la categoría es NULL en la base de datos, no se asigna ninguna categoría al producto
                    if (datos.Lector["Categoria"] == DBNull.Value)
                    {
                        aux.Categoria = null;
                    }
                    else
                    {
                        aux.Categoria = new Categoria();
                        aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    }

                    aux.Precio = (decimal)datos.Lector["Precio"];

                    // Si no hay una imagen registrada (valor NULL en la base de datos), se asigna una imagen por defecto
                    if (datos.Lector["ImagenUrl"] != DBNull.Value)
                    {
                        aux.Imagenes = new List<Imagen>
                        {
                            new Imagen { UrlImagen = (string)datos.Lector["ImagenUrl"] }
                        };
                    }
                    else
                    {
                        aux.Imagenes = new List<Imagen>(); // para evitar errores de null
                    }

                    buscar.Add(aux);
                }

                return buscar;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }


        }

        public void Modificar(Producto art)
        {
            AccesoDatos datosModificados = new AccesoDatos();
            try
            {

                datosModificados.setearParametro("@id", art.Id);
                datosModificados.setearParametro("@cod", art.Codigo);
                datosModificados.setearParametro("@nom", art.Nombre);
                datosModificados.setearParametro("@desc", art.Descripcion);
                datosModificados.setearParametro("@Mrca", art.Marca.Descripcion);
                datosModificados.setearParametro("@Ctgria", art.Categoria.Descripcion);
                string urlImagen = (art.Imagenes != null && art.Imagenes.Count > 0) ? art.Imagenes[0].UrlImagen : "";
                datosModificados.setearParametro("@img", urlImagen);
                datosModificados.setearParametro("@Prec", art.Precio);

                datosModificados.setearConsulta("update IMAGENES SET ImagenUrl=@img WHERE IdArticulo=@id");
                datosModificados.ejecutarAccion();
                datosModificados.cerrarConexion();
                datosModificados.setearConsulta("UPDATE ARTICULOS SET IdMarca = MARCAS.Id FROM ARTICULOS INNER JOIN MARCAS ON ARTICULOS.Id=@id WHERE MARCAS.Descripcion=@Mrca");
                datosModificados.ejecutarAccion();
                datosModificados.cerrarConexion();
                datosModificados.setearConsulta("UPDATE ARTICULOS SET IdCategoria = CATEGORIAS.Id FROM ARTICULOS INNER JOIN CATEGORIAS ON ARTICULOS.Id=@id WHERE CATEGORIAS.Descripcion=@Ctgria");
                datosModificados.ejecutarAccion();
                datosModificados.cerrarConexion();
                datosModificados.setearConsulta("UPDATE ARTICULOS SET Codigo=@cod, Nombre=@nom, Descripcion=@desc, Precio=@Prec WHERE Id=@id");
                datosModificados.ejecutarAccion();
                datosModificados.cerrarConexion();

            }
            catch (Exception ex)
            { throw ex; }
            //finally { datosModificados.cerrarConexion(); }

        }


        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM ARTICULOS WHERE Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex; // Devolvés la excepción al que llame el método
            }
            finally
            {
                datos.cerrarConexion();
            }
        }



    }




}

