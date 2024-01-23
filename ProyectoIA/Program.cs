using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Nodo
    {
        int[,] casillas = new int[3, 3]; // indico cuantas casillas hay [,] eso indica que es bidimensional

        public Nodo()
        {
            int valor = 0; // la primera casilla empieza en 0
            for (int fila = 0; fila < 3; fila++)
            {
                for (int columna = 0; columna < 3; columna++)
                {
                    casillas[fila, columna] = valor++;
                }
            }

        }
        public Nodo(int fila0columna0, int fila0columna1, int fila0columna2, int fila1columna0, int fila1columna1, int fila1columna2, int fila2columna0, int fila2columna1, int fila2columna2)
        {
            casillas[0, 0] = fila0columna0;
            casillas[0, 1] = fila0columna1;
            casillas[0, 2] = fila0columna2;
            casillas[1, 0] = fila1columna0;
            casillas[1, 1] = fila1columna1;
            casillas[1, 2] = fila1columna2;
            casillas[2, 0] = fila2columna0;
            casillas[2, 1] = fila2columna1;
            casillas[2, 2] = fila2columna2;

            //para colocar como quieras las casillas
        }

        public void Desordenar()
        {
            // De momento no lo usamos
        }
        public string ImprimeElNodoEnPantalla()
        {
            string nodoImpreso = "\n"; //para que comience con un retorno de carro para forzar a un cambio de linea
            for (int fila = 0; fila < 3; fila++)
            { //recorre todas las casillas para mostrar el valor que hay en cada una
                for (int columna = 0; columna < 3; columna++)
                {
                    nodoImpreso += casillas[fila, columna] + ",";
                }
                nodoImpreso += "\n";
            }
            return nodoImpreso;
        }

        public bool EsIgualA(Nodo otroNodo)
        {
            for (int fila = 0; fila < 3; fila++)
                for (int columna = 0; columna < 3; columna++)
                {
                    if (casillas[fila, columna] != otroNodo.casillas[fila, columna])
                        return false; //compara los dos nodos y si en algun momento es distinta devuelve falso diciendo que no son iguales
                }
            return true;
        }

        public bool EsMeta()
        {
            int valor = 0;
            for (int fila = 0; fila < 3; fila++)
            {
                for (int columna = 0; columna < 3; columna++)
                {
                    if (valor != casillas[fila, columna])
                        return false; //si el nodo no sigue el orden del nodo meta devuelve falso
                    valor++;
                }
            }
            return true;
        }

        public (int, int) PosicionHueco() // este metodo devolvera dos enteros con la casilla donde esta el hueco, que sirve saber a donde me puedo desplazar con el cero y donde no solo me puedo desplazar en cruz
        {
            int huecoEnFila = 0, huecoEnColumna = 0;
            for (int fila = 0; fila < 3; fila++)
            {
                for (int columna = 0; columna < 3; columna++)
                {
                    if (casillas[fila, columna] == 0)
                    {
                        huecoEnFila = fila;
                        huecoEnColumna = columna;
                    }
                }
            }
            return (huecoEnFila, huecoEnColumna);
        }
        public List<Nodo> ObtenerSucesores() // metodo que dice los movimientos posibles que tiene el 0 y te los va a devolver en forma de lista y como minimo serian entre 2 - 4
        {
            //Lista de sucesores a obtener y devolver
            List<Nodo> sucesores = new List<Nodo>();
            //En función de la posición actual del Hueco se determina hacia dónde podrá moverse
            //Obteniéndose el sucesor que corresponda a cada caso y añadiéndolos a la lista de sucesores, busca donde esta el hueco en el nodo actual
            var (filaActualHueco, columnaActualHueco) = this.PosicionHueco();
            int filaNuevaHueco, columnaNuevaHueco;

            if (filaActualHueco == 0 || filaActualHueco == 1)
            {
                filaNuevaHueco = filaActualHueco + 1;  //Se crea un sucesor en el que el hueco se mueve a la fila de abajo
                columnaNuevaHueco = columnaActualHueco; //manteniéndose en la misma columna

                Nodo sucesorAbajo = new Nodo();
                sucesorAbajo.CopiarEstado(this.Sucesor(filaActualHueco, columnaActualHueco, filaNuevaHueco, columnaNuevaHueco));
                //Se añade el sucesor recién creado a la lista de nodos sucesores
                sucesores.Add(sucesorAbajo);
            }

            //El hueco podrá moverse hacia arriba si está en la fila 1 ó 2
            if (filaActualHueco == 1 || filaActualHueco == 2)
            {
                //Se crea un sucesor en el que el hueco se mueve a la fila de arriba
                //manteniéndose en la misma columna
                filaNuevaHueco = filaActualHueco - 1; // para subir se restan y para bajar se suman, si vas para izquierda -1 y para derecha +1
                columnaNuevaHueco = columnaActualHueco;

                Nodo sucesorArriba = new Nodo();
                sucesorArriba.CopiarEstado(this.Sucesor(filaActualHueco, columnaActualHueco, filaNuevaHueco, columnaNuevaHueco));
                //Se añade el sucesor recién creado a la lista de nodos sucesores
                sucesores.Add(sucesorArriba); // se ha intercambiado un cero con el numero que estaba en la fila de arriba

            }
            //El hueco podrá moverse hacia la derecha si está en la columna 0 ó 1
            if (columnaActualHueco == 0 || columnaActualHueco == 1)
            {
                //Se crea un sucesor en el que el hueco se mueve a la columna de la derecha
                //manteniéndose en la misma fila
                filaNuevaHueco = filaActualHueco;
                columnaNuevaHueco = columnaActualHueco + 1;

                Nodo sucesorDerecha = new Nodo();
                sucesorDerecha.CopiarEstado(this.Sucesor(filaActualHueco, columnaActualHueco, filaNuevaHueco, columnaNuevaHueco));
                //Se añade el sucesor recién creado a la lista de nodos sucesores
                sucesores.Add(sucesorDerecha);
            }
            //El hueco podrá moverse hacia la izquierda si está en la columna 1 ó 2
            if (columnaActualHueco == 1 || columnaActualHueco == 2)
            {
                //Se crea un sucesor en el que el hueco se mueve a la columna de la izquierda
                //manteniéndose en la misma fila
                filaNuevaHueco = filaActualHueco;
                columnaNuevaHueco = columnaActualHueco - 1;

                Nodo sucesorIzquierda = new Nodo();
                sucesorIzquierda.CopiarEstado(this.Sucesor(filaActualHueco, columnaActualHueco, filaNuevaHueco, columnaNuevaHueco));
                //Se añade el sucesor recién creado a la lista de nodos sucesores
                sucesores.Add(sucesorIzquierda);
            }
            return sucesores;

        }
        private Nodo Sucesor(int filaActual, int columnaActual, int filaNueva, int columnaNueva) // te devuelve el nodo sucesor con el cmabio del 0 ejecutado
        {
            Nodo nodoTemporal = new Nodo(); // creamos un nodo temporal para que no se pierda el nodo original que es antes de desplazar el 0
            nodoTemporal.CopiarEstado(this); // hacer una copia temporal para no perder este nodo
            int temporal = nodoTemporal.casillas[filaNueva, columnaNueva];

            nodoTemporal.casillas[filaNueva, columnaNueva] = 0; //Coloca el hueco en su nueva posición
            nodoTemporal.casillas[filaActual, columnaActual] = temporal;

            return nodoTemporal; // devuelveme con el 0 ya desplazado
        }

        public void CopiarEstado(Nodo nodoOriginal)
        {
            for (int fila = 0; fila < 3; fila++) //copia los valores de las casillas de un nodo en el otro nodo
            {
                for (int columna = 0; columna < 3; columna++)
                {
                    casillas[fila, columna] = nodoOriginal.casillas[fila, columna];
                }
            }
        }
    }


    public class Puzzle
    {
        static void Main(string[] args)
        {
            StreamWriter ficheroPruebas = new StreamWriter("comprobaciones.txt");

            Nodo nodoActual = new Nodo();
            Nodo nodoInicial = new Nodo(6, 5, 4,
                                        1, 3, 2,
                                        8, 0, 7);
            Console.WriteLine("Nodo inicial:\n" + nodoInicial.ImprimeElNodoEnPantalla());

            List<Nodo> nodosAbiertos = new List<Nodo>(); // nodos que nos falta por visitar
            List<Nodo> nodosCerrados = new List<Nodo>(); // nodos o posibibles movimientos que ya hemos visitado

            nodosAbiertos.Add(nodoInicial); // que donde empieza el cero a la vez esta abierto porque no hemos empezado pero esta visitado porque ya estamos en el al empezar
            nodosCerrados.Add(nodoInicial);

            int numeroPasadas = 0; // cuantas veces va a hacer el bucle hasta encontrar el nodo meta, empezamos en cero porque no hemos iniciado a buscar

            Console.WriteLine("Buscando el nodo META. Puede tardar varios minutos...");

            while (nodosAbiertos.Count > 0) // mientras queden nodos abiertos seguimos repitiendo el bucle y seguimos buscando la posible solucion
            {
                //Copia en actual el primer nodo Abierto
                nodoActual.CopiarEstado(nodosAbiertos[0]); //PONEMOS EL CERO PORQUE SIGNIFICA QUE DE LA LISTA QUE ESCOJA EL PRIMER NODO QUE ES EL 0
                ficheroPruebas.WriteLine("Paso " + ++numeroPasadas + ". Num. abiertos: " + nodosAbiertos.Count + ". Num cerrados: " + nodosCerrados.Count + " Nodo actual:" + nodoActual.ImprimeElNodoEnPantalla());

                nodosAbiertos.RemoveAt(0); // remueve de la lista el nodo primero porque ya esta visitado al estar en el

                Nodo nodoACerrar = new Nodo();
                nodoACerrar.CopiarEstado(nodoActual);
                nodosCerrados.Add(nodoACerrar);//Añade el nodo actual a la lista de Cerrados

                if (nodoActual.EsMeta())
                {
                    Console.WriteLine("¡Nodo META encontrado! Número de pasadas: " + numeroPasadas);
                    ficheroPruebas.Close(); // cuando acabe el programa encuentre o no la solucion cierra el fichero
                    Console.WriteLine("Pulsa cualquier tecla para salir");
                    Console.ReadKey();
                    return; // si llega a ser meta rompe el bucle porque has encontrado la solucion
                }
                else//Si todavía no se ha llegado al nodo meta
                {
                    //Se obtienen todos los posibles sucesores del nodo actual
                    List<Nodo> posiblesSucesores = new List<Nodo>();
                    posiblesSucesores = nodoActual.ObtenerSucesores();

                    //Se añaden al final de la lista de Abiertos todos los posibles sucesores que no
                    //estén ya ni en la lista de Abiertos ni en la de Cerrados
                    foreach (Nodo n in posiblesSucesores)
                    {
                        if (n.EsMeta())
                        {
                            Console.WriteLine("¡Nodo META encontrado! Número de pasadas: " + numeroPasadas);
                            ficheroPruebas.Close(); // cuando acabe el programa encuentre o no la solucion cierra el fichero
                            Console.WriteLine("Pulsa cualquier tecla para salir");
                            Console.ReadKey();
                            return; // si n es meta termina el programa tambien porque hemos llegado a la soluccion
                        }
                        if (!EstaEnLaLista(n, nodosCerrados) && !EstaEnLaLista(n, nodosAbiertos)) //si no esta ni en la lista de abiertos ni de cerrados añadelo a la lista de abiertos
                            nodosAbiertos.Add(n);
                    }
                }
            }

            Console.WriteLine("El nodo meta no se ha encontrado");
            ficheroPruebas.Close(); // cuando acabe el programa encuentre o no la solucion cierra el fichero
            Console.WriteLine("Pulsa cualquier tecla para salir");
            Console.ReadKey();
        }


        static bool EstaEnLaLista(Nodo nodoAComprobar, List<Nodo> listaDeNodos) //comprueba si un nodo esta dentro de una lista
        {
            foreach (Nodo n in listaDeNodos)
            {
                if (nodoAComprobar.EsIgualA(n))
                    return true;
            }
            return false;
        }
    }
}