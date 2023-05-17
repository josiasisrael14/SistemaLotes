using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;


namespace SistemaLotes.Models
{
    public class db
    {

        private readonly string _conexion;

        public db(IConfiguration aplication)
        {

            _conexion = aplication.GetConnectionString("conexion");


        }



        public DataTable login(entidad acceso)
        {


            using (SqlConnection sql = new SqlConnection(_conexion))
            {

                using (SqlCommand com = new SqlCommand("SP_LOGIN", sql))
                {

                    int ver;
                    //SqlCommand com = new SqlCommand("sp_loguer",con);
                   
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@logeo", acceso.logeo);
                    com.Parameters.AddWithValue("@contrazeña", acceso.contrazeña);
                    com.CommandTimeout = 0;
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;

                  
                }

            }

        }



        public DataTable SP_ListarEtapas()
        {


            using (SqlConnection sql = new SqlConnection(_conexion))
            {

                using (SqlCommand com = new SqlCommand("SP_LISTAR_ETAPAS", sql))
                {

                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;


                }

            }

        }


        public DataTable SP_LISTARLOTES()
        {


            using (SqlConnection sql = new SqlConnection(_conexion))
            {

                using (SqlCommand com = new SqlCommand("SP_LISTARLOTES", sql))
                {
                    com.CommandTimeout = 0;
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;


                }

            }

        }

        public DataTable SP_LISTAR_LOTES_SEGUN_ETAPAS(entidad datos)
        {


            using (SqlConnection sql = new SqlConnection(_conexion))
            {

                using (SqlCommand cmd = new SqlCommand("SP_LISTAR_LOTES_SEGUN_ETAPAS", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idetapas", datos.idetapas);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;


                }

            }

        }



        public DataTable SP_REPORTEVENTAUSUARIO(entidad datos)
        {


            using (SqlConnection sql = new SqlConnection(_conexion))
            {

                using (SqlCommand cmd = new SqlCommand("SP_REPORTEVENTAUSUARIO", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idusuario", datos.idusuario);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;


                }

            }

        }




        public int SP_CONTARLOTES()
        {

            using (SqlConnection sql = new SqlConnection(_conexion))

            {
                using (SqlCommand cmd = new SqlCommand("SP_CONTARLOTES", sql))
                {

                    sql.Open();
                    int con = (int)cmd.ExecuteScalar();

                    return con;

                }

            }



        }


        public int SP_CONTARETAPAS()
        {

            using (SqlConnection sql = new SqlConnection(_conexion))

            {
                using (SqlCommand cmd = new SqlCommand("SP_CONTARETAPAS", sql))
                {

                    sql.Open();
                    int con = (int)cmd.ExecuteScalar();

                    return con;

                }

            }



        }




        public int SP_CONTARUSUARIOS()
        {

            using (SqlConnection sql = new SqlConnection(_conexion))

            {
                using (SqlCommand cmd = new SqlCommand("SP_CONTARUSUARIOS", sql))
                {

                    sql.Open();
                    int con = (int)cmd.ExecuteScalar();

                    return con;

                }

            }



        }






        public DataTable SP_LISTARVENTASCONTADO()
        {


            using (SqlConnection sql = new SqlConnection(_conexion))
            {

                using (SqlCommand com = new SqlCommand("SP_LISTARVENTASCONTADO", sql))
                   
                {
                    com.CommandTimeout = 0;
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;


                }

            }

        }





        public DataTable SP_LISTARVENTASSEPARADO()
        {


            using (SqlConnection sql = new SqlConnection(_conexion))
            {

                using (SqlCommand com = new SqlCommand("SP_LISTARVENTASSEPARADO", sql))
                {
                    com.CommandTimeout = 0;
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;


                }

            }

        }












        public string SP_insertar_etapas(entidad datos)
        {
            using (SqlConnection sql = new SqlConnection(_conexion))
            {
                using (SqlCommand cmd = new SqlCommand("SP_INSERTAR_ETAPAS", sql))
                {
                    int ver;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NombreEtapa", datos.NombreEtapa);
                    cmd.Parameters.AddWithValue("@Descripcion", datos.Descripcion);
                    sql.Open();
                    try
                    {
                        ver = cmd.ExecuteNonQuery();




                    }
                    catch (Exception ex)
                    {

                        //MessageBox.Show("Erro: " + ex.ToString());




                    }
                    return null;

                }



            }
        }









        public DataTable ListarUsuario()
        {

            using (SqlConnection sql=new SqlConnection(_conexion))
            {
                using (SqlCommand com = new SqlCommand("SP_LISTARUSUARIOS", sql))
                {
                    com.CommandTimeout = 0;
                    SqlDataAdapter da=new SqlDataAdapter(com);

                    DataTable dt = new DataTable();

                    da.Fill(dt);
                    return dt;

                }


            }
        }

        public DataTable SP_LISTARUSUARIOSID(entidad datos)
        {


            using (SqlConnection sql = new SqlConnection(_conexion))
            {

                using (SqlCommand cmd = new SqlCommand("SP_LISTARUSUARIOSID", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idusuario", datos.idusuario);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;


                }

            }

        }





        public DataTable ListarClientes()
        {

            using (SqlConnection sql = new SqlConnection(_conexion))
            {
                using (SqlCommand com = new SqlCommand("SP_LISTARCLIENTE", sql))
                {
                    com.CommandTimeout = 0;
                    SqlDataAdapter da = new SqlDataAdapter(com);

                    DataTable dt = new DataTable();

                    da.Fill(dt);
                    return dt;

                }


            }
        }





        public string SP_INSERTARORUPDATE(entidad datos)
        {
            using (SqlConnection sql = new SqlConnection(_conexion))
            {
                using (SqlCommand cmd = new SqlCommand("SP_INSERTARORUPDATE", sql))
                {
                    int ver;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idusuario", datos.idusuario);
                    cmd.Parameters.AddWithValue("@nombre", datos.nombre);
                    cmd.Parameters.AddWithValue("@logeo", datos.logeo);
                    cmd.Parameters.AddWithValue("@fechacreado", datos.fechacreado);
                    cmd.Parameters.AddWithValue("@contrazeña", datos.contrazeña);
                    cmd.Parameters.AddWithValue("@imagen", datos.foto);
                    cmd.Parameters.AddWithValue("@idtipo", datos.idtipo);
                    cmd.Parameters.AddWithValue("@operacion", datos.operacion);

                    sql.Open();
                    try
                    {
                        ver = cmd.ExecuteNonQuery();




                    }
                    catch (Exception ex)
                    {

                        //MessageBox.Show("Erro: " + ex.ToString());




                    }
                    return null;

                }



            }
        }








        public DataTable SP_ListarEtapasSelectItem()
        {

            using (SqlConnection sql = new SqlConnection(_conexion))
            {
                using (SqlCommand com = new SqlCommand("SP_LISTARETAPAS_SELECTLISTITEM", sql))
                {

                    SqlDataAdapter da = new SqlDataAdapter(com);

                    DataTable dt = new DataTable();

                    da.Fill(dt);
                    return dt;

                }


            }
        }


        public DataTable SP_ListarLotesSelectItem()
        {

            using (SqlConnection sql = new SqlConnection(_conexion))
            {
                using (SqlCommand com = new SqlCommand("SP_LISTARLOTES_SELECTLISTITEM", sql))
                {

                    SqlDataAdapter da = new SqlDataAdapter(com);

                    DataTable dt = new DataTable();

                    da.Fill(dt);
                    return dt;

                }


            }
        }


        public DataTable SP_CREARVENTASCONTADOLISTARDATOS(entidad datos)
        {


            using (SqlConnection sql = new SqlConnection(_conexion))
            {

                using (SqlCommand cmd = new SqlCommand("SP_CREARVENTASCONTADOLISTARDATOS", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idlotes", datos.idlotes);
                    cmd.Parameters.AddWithValue("@idetapas", datos.idetapas);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;


                }

            }



        }



        public DataTable SP_VALIDARDATOSEXISTENTES(entidad datos)
        {


            using (SqlConnection sql = new SqlConnection(_conexion))
            {

                using (SqlCommand cmd = new SqlCommand("SP_VALIDARDATOSEXISTENTES", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombrelotes", datos.nombrelotes);
                    cmd.Parameters.AddWithValue("@idetapas", datos.idetapas);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;


                }

            }



        }




        public DataTable SP_VALIDARNOMBRE(entidad datos)
        {



            if (string.IsNullOrEmpty(datos.NombreEtapa))
            {

                DataTable dt = new DataTable();

                return dt;


            }
            else { 


            using (SqlConnection sql = new SqlConnection(_conexion))
            {

                using (SqlCommand cmd = new SqlCommand("SP_VALIDARNOMBRE", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NombreEtapa", datos.NombreEtapa);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;


                }

            }

            }



        }







        public DataTable SP_CREATEVENTASSEPARADOLISTARDATOS(entidad datos)
        {


            using (SqlConnection sql = new SqlConnection(_conexion))
            {

                using (SqlCommand cmd = new SqlCommand("SP_CREATEVENTASSEPARADOLISTARDATOS", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idlotes", datos.idlotes);
                    cmd.Parameters.AddWithValue("@idetapas", datos.idetapas);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;


                }

            }



        }








        public DataTable SP_LISTARCLIENTE()
        {

            using (SqlConnection sql = new SqlConnection(_conexion))
            {
                using (SqlCommand com = new SqlCommand("SP_LISTARCLIENTE", sql))
                {

                    SqlDataAdapter da = new SqlDataAdapter(com);

                    DataTable dt = new DataTable();

                    da.Fill(dt);
                    return dt;

                }


            }
        }

       


        public string SP_INSERTAR_VENTASCONTADO(entidad datos)
        {
            using (SqlConnection sql = new SqlConnection(_conexion))
            {
                using (SqlCommand cmd = new SqlCommand("SP_INSERTAR_VENTASCONTADO", sql))
                {
                    int ver;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@idlotes", datos.idlotes);
                    cmd.Parameters.AddWithValue("@idusuario", datos.idusuario);
                    cmd.Parameters.AddWithValue("@preciocontado", datos.preciocontado);
                    cmd.Parameters.AddWithValue("@imagenlotes", datos.imagenlotes1);
                    cmd.Parameters.AddWithValue("@proyectolotes", datos.ImagenProyecto);
                    cmd.Parameters.AddWithValue("@idcliente", datos.idcliente);
                    cmd.Parameters.AddWithValue("@voucher", datos.voucher);
                    cmd.Parameters.AddWithValue("@nombrelotes", datos.nombrelotes);
                    cmd.Parameters.AddWithValue("@fechaventa", datos.fechaventa);
                    cmd.Parameters.AddWithValue("@idetapas", datos.idetapas);

                    sql.Open();
                    try
                    {
                        ver = cmd.ExecuteNonQuery();




                    }
                    catch (Exception ex)
                    {

                        //MessageBox.Show("Erro: " + ex.ToString());




                    }
                    return null;

                }



            }
        }

        public DataTable SP_LISTAREMPLEADOS()
        {

            using (SqlConnection sql = new SqlConnection(_conexion))
            {

                using (SqlCommand com = new SqlCommand("SP_LISTAREMPLEADOS", sql))
                {

                    //SqlCommand com = new SqlCommand("LISTAREMPLEADOS", sql);
                    SqlDataAdapter da = new SqlDataAdapter(com);

                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    return dt;
                }

            }

        }



        public string SP_INSERTAR_VENTASSEPARADO(entidad datos)
        {
            using (SqlConnection sql = new SqlConnection(_conexion))
            {
                using (SqlCommand cmd = new SqlCommand("SP_INSERTAR_VENTASSEPARADO", sql))
                {
                    int ver;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@idlotes", datos.idlotes);
                    cmd.Parameters.AddWithValue("@idusuario", datos.idusuario);
                    cmd.Parameters.AddWithValue("@preciocontado", datos.preciocontado);
                    cmd.Parameters.AddWithValue("@precioinicial", datos.PrecioInicial);
                    cmd.Parameters.AddWithValue("@letraspagar", datos.LetrasPagar);
                    cmd.Parameters.AddWithValue("@restante", datos.restante);
                    cmd.Parameters.AddWithValue("@imagenlotes", datos.imagenlotes1);
                    cmd.Parameters.AddWithValue("@proyectolotes", datos.ImagenProyecto);
                    cmd.Parameters.AddWithValue("@idcliente", datos.idcliente);
                    cmd.Parameters.AddWithValue("@voucher", datos.voucher);
                    cmd.Parameters.AddWithValue("@nombrelotes", datos.nombrelotes);
                    cmd.Parameters.AddWithValue("@fechaventaS", datos.fechaventa);
                    cmd.Parameters.AddWithValue("@idetapas", datos.idetapas);
                    cmd.Parameters.AddWithValue("@cuotamonto", datos.montopagar);
                    cmd.Parameters.AddWithValue("@letrasrestante", datos.letrasrestante);

                    sql.Open();
                    try
                    {
                        ver = cmd.ExecuteNonQuery();




                    }
                    catch (Exception ex)
                    {

                        //MessageBox.Show("Erro: " + ex.ToString());




                    }
                    return null;

                }



            }
        }





        public string SP_INSERTTRANSACCION(entidad datos)
        {
            using (SqlConnection sql = new SqlConnection(_conexion))
            {
                using (SqlCommand cmd = new SqlCommand("SP_INSERTTRANSACCION", sql))
                {
                    int ver;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@nombretransaccion", datos.nombretransaccion);
                    cmd.Parameters.AddWithValue("@idetapas", datos.idetapas);
                    cmd.Parameters.AddWithValue("@NombreEtapa", datos.etapa);
                    cmd.Parameters.AddWithValue("@idlotes", datos.idlotes);
                    cmd.Parameters.AddWithValue("@nombrelotes", datos.nombrelotes);
                    cmd.Parameters.AddWithValue("@imagenlotes", datos.imagenlotes1);
                    cmd.Parameters.AddWithValue("@PrecioContado", datos.preciocontado);
                    cmd.Parameters.AddWithValue("@PrecioInicial", datos.PrecioInicial);
                    cmd.Parameters.AddWithValue("@LetrasPagar", datos.LetrasPagar);
                    cmd.Parameters.AddWithValue("@restante", datos.restante);
                    cmd.Parameters.AddWithValue("@idusuario", datos.idusuario);
                    cmd.Parameters.AddWithValue("@idcliente", datos.idcliente);
                    cmd.Parameters.AddWithValue("@fechaventa", datos.fechaventaS);
                    cmd.Parameters.AddWithValue("@estado", datos.estadotransaccion);
                    cmd.Parameters.AddWithValue("@voucher", datos.voucher);
                    cmd.Parameters.AddWithValue("@cuotamonto", datos.montopagar);
                   



                    sql.Open();
                    try
                    {
                        ver = cmd.ExecuteNonQuery();




                    }
                    catch (Exception ex)
                    {

                        //MessageBox.Show("Erro: " + ex.ToString());




                    }
                    return null;

                }



            }
        }





        public string SP_PAGARVENTASSEPARADO(entidad datos)
        {
            using (SqlConnection sql = new SqlConnection(_conexion))
            {
                using (SqlCommand cmd = new SqlCommand("SP_PAGARVENTASSEPARADO", sql))
                {
                    int ver;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@idventassepa", datos.idventassepa);
                    cmd.Parameters.AddWithValue("@restante", datos.restante);
                    cmd.Parameters.AddWithValue("@letrasrestante", datos.letras);
                    cmd.Parameters.AddWithValue("@idlotes", datos.idlotes);
                   
                 

                    sql.Open();
                    try
                    {
                        ver = cmd.ExecuteNonQuery();




                    }
                    catch (Exception ex)
                    {

                        //MessageBox.Show("Erro: " + ex.ToString());




                    }
                    return null;

                }



            }
        }















        public DataTable SP_LISTARTRANSACCION()
        {

            using (SqlConnection sql = new SqlConnection(_conexion))
            {
                using (SqlCommand com = new SqlCommand("SP_LISTARTRANSACCION", sql))
                {

                    SqlDataAdapter da = new SqlDataAdapter(com);

                    DataTable dt = new DataTable();

                    da.Fill(dt);
                    return dt;

                }


            }
        }



        public DataTable SP_LISTARDETALLETRANSACCION(entidad datos)
        {


            using (SqlConnection sql = new SqlConnection(_conexion))
            {

                using (SqlCommand cmd = new SqlCommand("SP_LISTARDETALLETRANSACCION", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idtransaccion", datos.idtransaccion);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;


                }

            }



        }



        public DataTable SP_VENTASSEPARADOPAGAR(entidad datos)
        {


            using (SqlConnection sql = new SqlConnection(_conexion))
            {

                using (SqlCommand cmd = new SqlCommand("SP_VENTASSEPARADOPAGAR", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idventassepa", datos.idventassepa);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;


                }

            }



        }









        public DataTable SP_LISTARTRANSACCIONPARAELIMINAR(entidad datos)
        {
            using (SqlConnection sql = new SqlConnection(_conexion))
            {

                using (SqlCommand cmd = new SqlCommand("SP_LISTARTRANSACCIONPARAELIMINAR", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idtransaccion", datos.idtransaccion);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;


                }

            }



        }



        public string SP_INSERTARLOTES(entidad datos)
        {
            using (SqlConnection sql = new SqlConnection(_conexion))
            {
                using (SqlCommand cmd = new SqlCommand("SP_INSERTARLOTES", sql))
                {
                    int ver;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@idetapas", datos.idetapas);
                    cmd.Parameters.AddWithValue("@estado", datos.estado);
                    cmd.Parameters.AddWithValue("@colindantes", datos.colindantes);
                    cmd.Parameters.AddWithValue("@PrecioContado", datos.preciocontado);
                    cmd.Parameters.AddWithValue("@PrecioInicial", datos.PrecioInicial);
                    cmd.Parameters.AddWithValue("@LetrasPagar", datos.LetrasPagar);
                    cmd.Parameters.AddWithValue("@ImagenLotes", datos.ImagenLotes);
                    cmd.Parameters.AddWithValue("@ImagenProyecto", datos.ImagenProyecto);
                    cmd.Parameters.AddWithValue("@nombrelotes", datos.nombrelotes);
                    cmd.Parameters.AddWithValue("@restante", datos.restante);


                    sql.Open();
                    try
                    {
                        ver = cmd.ExecuteNonQuery();




                    }
                    catch (Exception ex)
                    {

                        //MessageBox.Show("Erro: " + ex.ToString());




                    }
                    return null;

                }



            }
        }




        public string SP_RECHAZARTRANSACCION(entidad datos)
        {
            using (SqlConnection sql = new SqlConnection(_conexion))
            {
                using (SqlCommand cmd = new SqlCommand("SP_RECHAZARTRANSACCION", sql))
                {
                    int ver;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@idlotes", datos.idlotes);
                  
                    sql.Open();
                    try
                    {
                        ver = cmd.ExecuteNonQuery();


                    }
                    catch (Exception ex)
                    {

                        //MessageBox.Show("Erro: " + ex.ToString());




                    }
                    return null;

                }



            }
        }





        public string SP_INSERTARCLIENTE(entidad datos)
        {
            using (SqlConnection sql = new SqlConnection(_conexion))
            {
                using (SqlCommand cmd = new SqlCommand("SP_INSERTARCLIENTES", sql))
                {
                    int ver;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@nombrecliente", datos.nombrecliente);
                    cmd.Parameters.AddWithValue("@apellidos", datos.apellidosgeneral);
                    cmd.Parameters.AddWithValue("@celular", datos.celular);
                    cmd.Parameters.AddWithValue("@direccion", datos.direccion);
                    cmd.Parameters.AddWithValue("@dni", datos.Dni);
                    sql.Open();
                    try
                    {
                        ver = cmd.ExecuteNonQuery();


                    }
                    catch (Exception ex)
                    {

                        //MessageBox.Show("Erro: " + ex.ToString());




                    }
                    return null;

                }



            }
        }





    }
}
