using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace andre
{
    class Program
    {
        /***********************************************************
        Creador: Mauricio Alejandro Lopez
        Fecha ultima modifiecacion: 22/11/18
        Crado para: Andre Fuentes
        Descripcion: Juego sencillo en cosola que introduce un heroe
        que esta tratando de salvar a la princesa celestia en mapa
        aleatorio de tres posibles tamaños
        ***********************************************************/
        static string[,] llenar_tablero(int columnas, int filas, string estrella, string jugador, string enemigo, string pared, int paredes, int enemigos)
        {
            string[,] tablero = new string[columnas, filas];
            Random ran = new Random();
            int contadory = 0;
            int contadorx = 0;
            int contadorenemigos = 0;
            int contadorparedes = 0;
            bool estrellas = false;
            bool jugadores = false;
            while (contadory < columnas)
            {
                contadorx = 0;
                while (contadorx < filas)
                {
                    int alazar = ran.Next(0, 11);
                    if (((contadorx == filas - 1) || (contadorx == filas - 2)) && (contadory == columnas - 1))
                    {
                        if (!estrellas)
                        {
                            tablero[contadory, contadorx] = estrella;
                            contadorx++;
                            estrellas = true;
                            continue;
                        }
                        if (!jugadores)
                        {
                            tablero[contadory, contadorx] = jugador;
                            contadorx++;
                            jugadores = true;
                            continue;
                        }
                    }
                    if (alazar == 0 && !estrellas)
                    {
                        estrellas = true;
                        tablero[contadory, contadorx] = estrella;
                    }
                    else if (alazar == 1 && !jugadores)
                    {
                        jugadores = true;
                        tablero[contadory, contadorx] = jugador;
                    }
                    else if ((alazar == 2 || alazar == 3 || alazar == 4) && contadorenemigos < enemigos)
                    {
                        tablero[contadory, contadorx] = enemigo;
                        contadorenemigos++;
                    }
                    else if ((alazar == 5 || alazar == 6 || alazar == 7) && contadorparedes < paredes)
                    {
                        tablero[contadory, contadorx] = pared;
                        contadorparedes++;
                    }
                    else
                    {
                        tablero[contadory, contadorx] = " ";
                    }
                    contadorx++;
                }
                contadory++;
            }
            return tablero;
        }
        static void Main(string[] args)
        {
            string nombre = "";
            int vida = 0;
            const string obstaculo = "█";
            const string enemigo = "¤";
            const string personaje = "£";
            const string meta = "®";
            bool Partida = false;
            Random next1 = new Random();
            void localizar(ref int y, ref int x, int tamaño_tablero, string[,] tablero)
            {
                for (int i = 0; i < tamaño_tablero; i++)
                {
                    for (int j = 0; j < tamaño_tablero; j++)
                    {
                        y = i;
                        x = j;
                        if (tablero[i, j] == personaje)
                        {
                            i = tamaño_tablero;
                            break;
                        }
                    }
                }
            }
            void Imprimir_tablero(string[,] tablero, int a)
            {
                for (int i = 0; i < a; i++)
                {
                    for (int j = 0; j < a; j++)
                    {
                        switch (tablero[i, j])
                        {
                            case " ":
                                Console.Write("(" + i + "," + j + ") Vacía. ");
                                break;
                            case obstaculo:
                                Console.Write("(" + i + "," + j + ") Obstáculo. ");
                                break;
                            case enemigo:
                                Console.Write("(" + i + "," + j + ") Enemigo. ");
                                break;
                            case personaje:
                                Console.Write("(" + i + "," + j + ") Personaje. ");
                                break;
                            case meta:
                                Console.Write("(" + i + "," + j + ") Meta. ");
                                break;
                        }
                    }
                    Console.WriteLine();
                }
                Console.ReadKey();
            }
            void movimiento(ref string[,] tablero,int tamaño_tablero, ref int life, ref bool gano, string obstaculos, string Meta, string player)
            {
                ConsoleKeyInfo tecla;
                Console.WriteLine("ingrese la opcion deseada");
                Console.WriteLine("(flecha arriba) Mover arriba");
                Console.WriteLine("(flecha derecha) Mover derecha");
                Console.WriteLine("(flecha izquierda) Mover izquierda");
                Console.WriteLine("(flecha abajo) Mover abajo ");
                Console.WriteLine();
                Console.WriteLine("(z) para atacar y luego precione la direccion ");
                tecla = Console.ReadKey();
                int posicion_y = 0;
                int posicion_x = 0;
                switch (tecla.Key)
                {
                    case ConsoleKey.UpArrow:
                        localizar(ref posicion_y, ref posicion_x, tamaño_tablero, tablero);
                        if (posicion_y != 0)
                        {
                            if (tablero[posicion_y - 1, posicion_x] == " ")
                            {
                                tablero[posicion_y - 1, posicion_x] = player;
                                tablero[posicion_y, posicion_x] = " ";
                            }
                            if (tablero[posicion_y - 1, posicion_x] == obstaculos)
                            {
                                Console.WriteLine("No se puede mover, hay un obstaculo en esa direccion");
                                Console.ReadLine();
                            }
                            if (tablero[posicion_y - 1, posicion_x] == enemigo)
                            {
                                Console.WriteLine("No se puede mover, hay un enemigo en esa direccion y le ha echo un ataque critico");
                                vida--;
                                Console.ReadLine();
                            }
                            if (tablero[posicion_y - 1, posicion_x] == Meta)
                            {
                                Console.WriteLine("En hora buena, ha conseguido salvar a celestia la princesa");
                                Console.ReadLine();
                                gano = true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("No se puede mover, esta intentando salir del tablero");
                            Console.ReadLine();
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        localizar(ref posicion_y, ref posicion_x, tamaño_tablero, tablero);
                        if (posicion_y != tamaño_tablero - 1)
                        {
                            if (tablero[posicion_y + 1, posicion_x] == " ")
                            {
                                tablero[posicion_y + 1, posicion_x] = player;
                                tablero[posicion_y, posicion_x] = " ";
                            }
                            if (tablero[posicion_y + 1, posicion_x] == obstaculos)
                            {
                                Console.WriteLine("No se puede mover, hay un obstaculo en esa direccion");
                                Console.ReadLine();
                            }
                            if (tablero[posicion_y + 1, posicion_x] == enemigo)
                            {
                                Console.WriteLine("No se puede mover, hay un enemigo en esa direccion y le ha echo un ataque critico");
                                vida--;
                                Console.ReadLine();
                            }
                            if (tablero[posicion_y + 1, posicion_x] == Meta)
                            {
                                Console.WriteLine("En hora buena, ha conseguido salvar a celestia la princesa");
                                Console.ReadLine();
                                gano = true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("No se puede mover, esta intentando salir del tablero");
                            Console.ReadLine();
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        localizar(ref posicion_y, ref posicion_x, tamaño_tablero, tablero);
                        if (posicion_x != tamaño_tablero - 1)
                        {
                            if (tablero[posicion_y, posicion_x + 1] == " ")
                            {
                                tablero[posicion_y, posicion_x + 1] = player;
                                tablero[posicion_y, posicion_x] = " ";
                            }
                            if (tablero[posicion_y, posicion_x + 1] == obstaculos)
                            {
                                Console.WriteLine("No se puede mover, hay un obstaculo en esa direccion");
                                Console.ReadLine();
                            }
                            if (tablero[posicion_y, posicion_x + 1] == enemigo)
                            {
                                Console.WriteLine("No se puede mover, hay un enemigo en esa direccion y le ha echo un ataque critico");
                                vida--;
                                Console.ReadLine();
                            }
                            if (tablero[posicion_y, posicion_x + 1] == Meta)
                            {
                                Console.WriteLine("En hora buena, ha conseguido salvar a celestia la princesa");
                                Console.ReadLine();
                                gano = true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("No se puede mover, esta intentando salir del tablero");
                            Console.ReadLine();
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        localizar(ref posicion_y, ref posicion_x, tamaño_tablero, tablero);
                        if (posicion_x != 0)
                        {
                            if (tablero[posicion_y, posicion_x - 1] == " ")
                            {
                                tablero[posicion_y, posicion_x - 1] = player;
                                tablero[posicion_y, posicion_x] = " ";
                            }
                            if (tablero[posicion_y, posicion_x - 1] == obstaculos)
                            {
                                Console.WriteLine("No se puede mover, hay un obstaculo en esa direccion");
                                Console.ReadLine();
                            }
                            if (tablero[posicion_y, posicion_x - 1] == enemigo)
                            {
                                Console.WriteLine("No se puede mover, hay un enemigo en esa direccion y le ha echo un ataque critico");
                                vida--;
                                Console.ReadLine();
                            }
                            if (tablero[posicion_y, posicion_x - 1] == Meta)
                            {
                                Console.WriteLine("En hora buena, ha conseguido salvar a celestia la princesa");
                                Console.ReadLine();
                                gano = true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("No se puede mover, esta intentando salir del tablero");
                            Console.ReadLine();
                        }
                        break;
                    case ConsoleKey.Z:
                        Console.WriteLine("Ingreso disparar.\nSeleccione la direccion de disparo");
                        tecla = Console.ReadKey();
                        switch (tecla.Key)
                        {
                            case ConsoleKey.UpArrow:
                                localizar(ref posicion_y, ref posicion_x, tamaño_tablero, tablero);
                                if (posicion_y != 0)
                                {
                                    if (tablero[posicion_y - 1, posicion_x] == enemigo)
                                    {
                                        tablero[posicion_y - 1, posicion_x] = " ";
                                    }
                                }
                                break;
                            case ConsoleKey.DownArrow:
                                localizar(ref posicion_y, ref posicion_x, tamaño_tablero, tablero);
                                if (posicion_y != tamaño_tablero - 1)
                                {
                                    if (tablero[posicion_y + 1, posicion_x] == enemigo)
                                    {
                                        tablero[posicion_y + 1, posicion_x] = " ";
                                    }
                                }
                                break;
                            case ConsoleKey.RightArrow:
                                localizar(ref posicion_y, ref posicion_x, tamaño_tablero, tablero);
                                if (posicion_x != tamaño_tablero - 1)
                                {
                                    if (tablero[posicion_y, posicion_x + 1] == enemigo)
                                    {
                                        tablero[posicion_y, posicion_x + 1] = " ";
                                    }
                                }
                                break;
                            case ConsoleKey.LeftArrow:
                                localizar(ref posicion_y, ref posicion_x, tamaño_tablero, tablero);
                                if (posicion_x != 0)
                                {
                                    if (tablero[posicion_y, posicion_x - 1] == enemigo)
                                    {
                                        tablero[posicion_y, posicion_x - 1] = " ";
                                    }
                                }
                                break;
                        }
                        break;
                }
            }
            string opcion;
            do
            {
                Console.Clear();
                if (!Partida)
                {
                    Console.Write("Ingrese nombre del personaje: ");
                    nombre = Console.ReadLine();
                    Console.WriteLine();
                }
                else if (Partida)
                {
                    Partida = false;
                }
                Console.WriteLine("si Desea jugar en algun tablero precione:\na) tablero 7X7\nb) tablero 10X10\nc) tablero 25X25");
                Console.WriteLine();
                Console.WriteLine("Si desea salir precione d");
                opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "a":
                        if (nombre != "" && nombre != " ")
                        {
                            bool vidas_infinitas = false;
                            string[,] tablero1 = new string[7, 7];
                            tablero1 = llenar_tablero(7,7,meta,personaje,enemigo,obstaculo,10,5);
                            if (vida == 0)
                            {
                                vida = 3;
                            }
                            while (!Partida)
                            {
                                Console.Clear();
                                int posicion_y = 0;
                                int posicion_x = 0;
                                localizar(ref posicion_y, ref posicion_x, 7, tablero1);
                                for (int i = -1; i <= 7; i++)
                                {
                                    for (int j = -1; j <= 7; j++)
                                    {
                                        if ((i == -1 && j == -1) || (i == -1 && j == 7) || (i == 7 && j == 7) || (i == 7 && j == -1))
                                        {
                                            Console.Write("#");
                                        }
                                        else if (i == -1 || i == 7)
                                        {
                                            Console.Write("-");
                                        }
                                        else if (j == -1 || j == 7)
                                        {
                                            Console.Write("|");
                                        }
                                        else if (tablero1[i, j] == meta)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.Write(tablero1[i, j]);
                                        }
                                        else if (tablero1[i, j] == obstaculo)
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.Write(tablero1[i, j]);
                                        }
                                        else
                                        {
                                            Console.Write(tablero1[i, j]);
                                        }
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    Console.WriteLine();
                                }
                                Console.WriteLine("a) Comandos");
                                Console.WriteLine("b) Imprimir tablero");
                                Console.WriteLine("c) Estatus personaje principal");
                                Console.WriteLine("d) Cheat Code");
                                Console.WriteLine("e) Terminar partida");
                                string op = Console.ReadLine();
                                if (op == "e")
                                {
                                    break;
                                }
                                if (op == "d")
                                {
                                    var tecla1 = Console.ReadKey();
                                    var tecla2 = Console.ReadKey();
                                    var tecla3 = Console.ReadKey();
                                    var tecla4 = Console.ReadKey();
                                    var tecla5 = Console.ReadKey();
                                    var tecla6 = Console.ReadKey();
                                    var tecla7 = Console.ReadKey();
                                    var tecla8 = Console.ReadKey();
                                    var tecla9 = Console.ReadKey();
                                    var tecla10 = Console.ReadKey();
                                    if((tecla1.Key == ConsoleKey.UpArrow) && (tecla2.Key == ConsoleKey.UpArrow) && (tecla3.Key == ConsoleKey.DownArrow) && (tecla4.Key == ConsoleKey.DownArrow) && (tecla5.Key == ConsoleKey.LeftArrow) && (tecla6.Key == ConsoleKey.RightArrow) && (tecla7.Key == ConsoleKey.LeftArrow) && (tecla8.Key == ConsoleKey.RightArrow) && (tecla9.Key == ConsoleKey.B) && (tecla10.Key == ConsoleKey.A))
                                    {
                                        vidas_infinitas = true;
                                    }
                                }
                                if (op == "c")
                                {
                                    Console.WriteLine("El personaje " + nombre);
                                    Console.WriteLine("Vida: " + vida);
                                    Console.WriteLine("Ubicacion: Columna " + posicion_y + ", Fila " + posicion_x);
                                    Console.ReadKey();
                                }
                                if (op == "b")
                                {
                                    Imprimir_tablero(tablero1, 4);
                                }
                                if (op == "a")
                                {
                                    movimiento(ref tablero1, 7, ref vida, ref Partida, obstaculo, meta, personaje);
                                }
                                if (vida == 0)
                                {
                                    Console.WriteLine("Game Over");
                                    Console.ReadKey();
                                    break;
                                }
                                else if(vidas_infinitas)
                                {
                                    vida = 999;
                                }
                            }
                        }
                        break;
                    case "b":
                        if (nombre != "" && nombre != " ")
                        {
                            bool vidas_infinitas = false;
                            string[,] tablero2 = new string[10, 10];
                            tablero2 = llenar_tablero(10, 10, meta, personaje, enemigo, obstaculo, 50, 30);
                            if (vida == 0)
                            {
                                vida = 3;
                            }
                            while (!Partida)
                            {
                                Console.Clear();
                                int posicion_y = 0;
                                int posicion_x = 0;
                                localizar(ref posicion_y, ref posicion_x, 10, tablero2);
                                for (int i = -1; i <= 10; i++)
                                {
                                    for (int j = -1; j <= 10; j++)
                                    {
                                        if ((i == -1 && j == -1) || (i == -1 && j == 10) || (i == 10 && j == 10) || (i == 10 && j == -1))
                                        {
                                            Console.Write("#");
                                        }
                                        else if (i == -1 || i == 10)
                                        {
                                            Console.Write("-");
                                        }
                                        else if (j == -1 || j == 10)
                                        {
                                            Console.Write("|");
                                        }
                                        else if (tablero2[i, j] == meta)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.Write(tablero2[i, j]);
                                        }
                                        else if (tablero2[i, j] == obstaculo)
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.Write(tablero2[i, j]);
                                        }
                                        else
                                        {
                                            Console.Write(tablero2[i, j]);
                                        }
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    Console.WriteLine();
                                }
                                Console.WriteLine("a) Comandos");
                                Console.WriteLine("b) Imprimir tablero");
                                Console.WriteLine("c) Estatus personaje principal");
                                Console.WriteLine("d) Cheat Code");
                                Console.WriteLine("e) Terminar partida");
                                string op = Console.ReadLine();
                                if (op == "e")
                                {
                                    break;
                                }
                                if (op == "d")
                                {
                                    var tecla1 = Console.ReadKey();
                                    var tecla2 = Console.ReadKey();
                                    var tecla3 = Console.ReadKey();
                                    var tecla4 = Console.ReadKey();
                                    var tecla5 = Console.ReadKey();
                                    var tecla6 = Console.ReadKey();
                                    var tecla7 = Console.ReadKey();
                                    var tecla8 = Console.ReadKey();
                                    var tecla9 = Console.ReadKey();
                                    var tecla10 = Console.ReadKey();
                                    if ((tecla1.Key == ConsoleKey.UpArrow) && (tecla2.Key == ConsoleKey.UpArrow) && (tecla3.Key == ConsoleKey.DownArrow) && (tecla4.Key == ConsoleKey.DownArrow) && (tecla5.Key == ConsoleKey.LeftArrow) && (tecla6.Key == ConsoleKey.RightArrow) && (tecla7.Key == ConsoleKey.LeftArrow) && (tecla8.Key == ConsoleKey.RightArrow) && (tecla9.Key == ConsoleKey.B) && (tecla10.Key == ConsoleKey.A))
                                    {
                                        vidas_infinitas = true;
                                    }
                                }
                                if (op == "c")
                                {
                                    Console.WriteLine("El personaje " + nombre);
                                    Console.WriteLine("Vida: " + vida);
                                    Console.WriteLine("Ubicacion: Columna " + posicion_y + ", Fila " + posicion_x);
                                    Console.ReadKey();
                                }
                                if (op == "b")
                                {
                                    Imprimir_tablero(tablero2, 10);
                                }
                                if (op == "a")
                                {
                                    movimiento(ref tablero2, 10, ref vida, ref Partida, obstaculo, meta, personaje);
                                }
                                if (vida == 0)
                                {
                                    Console.WriteLine("Game Over");
                                    Console.ReadKey();
                                    break;
                                }
                                else if (vidas_infinitas)
                                {
                                    vida = 999;
                                }
                            }
                        }
                        break;
                    case "c":
                        if (nombre != "" && nombre != " ")
                        {

                            bool vidas_infinitas = false;
                            string[,] tablero3 = new string[25, 25];
                            tablero3 = llenar_tablero(25, 25, meta, personaje, enemigo, obstaculo, 310, 190);
                            if (vida == 0)
                            {
                                vida = 3;
                            }
                            while (!Partida)
                            {
                                Console.Clear();
                                int posicion_y = 0;
                                int posicion_x = 0;
                                localizar(ref posicion_y, ref posicion_x, 25, tablero3);
                                for (int i = -1; i <= 25; i++)
                                {
                                    for (int j = -1; j <= 25; j++)
                                    {
                                        if ((i == -1 && j == -1) || (i == -1 && j == 25) || (i == 25 && j == 25) || (i == 25 && j == -1))
                                        {
                                            Console.Write("#");
                                        }
                                        else if (i == -1 || i == 25)
                                        {
                                            Console.Write("-");
                                        }
                                        else if (j == -1 || j == 25)
                                        {
                                            Console.Write("|");
                                        }
                                        else if (tablero3[i, j] == meta)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.Write(tablero3[i, j]);
                                        }
                                        else if (tablero3[i, j] == obstaculo)
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                                            Console.Write(tablero3[i, j]);
                                        }
                                        else
                                        {
                                            Console.Write(tablero3[i, j]);
                                        }
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    Console.WriteLine();
                                }
                                Console.WriteLine("a) Comandos");
                                Console.WriteLine("b) Imprimir tablero");
                                Console.WriteLine("c) Estatus personaje principal");
                                Console.WriteLine("d) Cheat Code");
                                Console.WriteLine("e) Terminar partida");
                                string op = Console.ReadLine();
                                if (op == "e")
                                {
                                    break;
                                }
                                if (op == "d")
                                {
                                    var tecla1 = Console.ReadKey();
                                    var tecla2 = Console.ReadKey();
                                    var tecla3 = Console.ReadKey();
                                    var tecla4 = Console.ReadKey();
                                    var tecla5 = Console.ReadKey();
                                    var tecla6 = Console.ReadKey();
                                    var tecla7 = Console.ReadKey();
                                    var tecla8 = Console.ReadKey();
                                    var tecla9 = Console.ReadKey();
                                    var tecla10 = Console.ReadKey();
                                    if ((tecla1.Key == ConsoleKey.UpArrow) && (tecla2.Key == ConsoleKey.UpArrow) && (tecla3.Key == ConsoleKey.DownArrow) && (tecla4.Key == ConsoleKey.DownArrow) && (tecla5.Key == ConsoleKey.LeftArrow) && (tecla6.Key == ConsoleKey.RightArrow) && (tecla7.Key == ConsoleKey.LeftArrow) && (tecla8.Key == ConsoleKey.RightArrow) && (tecla9.Key == ConsoleKey.B) && (tecla10.Key == ConsoleKey.A))
                                    {
                                        vidas_infinitas = true;
                                    }
                                }
                                if (op == "c")
                                {
                                    Console.WriteLine("El personaje " + nombre);
                                    Console.WriteLine("Vida: " + vida);
                                    Console.WriteLine("Ubicacion: Columna " + posicion_y + ", Fila " + posicion_x);
                                    Console.ReadKey();
                                }
                                if (op == "b")
                                {
                                    Imprimir_tablero(tablero3, 25);
                                }
                                if (op == "a")
                                {
                                    movimiento(ref tablero3, 25, ref vida, ref Partida, obstaculo, meta, personaje);
                                }
                                if (vida == 0)
                                {
                                    Console.WriteLine("Game Over");
                                    Console.ReadKey();
                                    break;
                                }
                            }
                            if(Partida)
                            {
                                Console.WriteLine("En hora buena logro pasar el nivel final, Puede ingresar en la opcion de ceat code\narriba,arriba,abajo,abajo,izquierda,derecha,izquierda,derecha,B,A\nSi ingrsa esto optendra vidas ilimitadas");
                            }
                        }
                        break;
                }
            } while (opcion != "d");
        }
    }
}
