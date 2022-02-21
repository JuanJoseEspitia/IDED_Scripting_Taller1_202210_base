using System.Collections.Generic;

namespace TestProject1
{
    internal class TestMethods
    {
        internal enum EValueType
        {
            Two,
            Three,
            Five,
            Seven,
            Prime
        }

        internal static Stack<int> GetNextGreaterValue(Stack<int> sourceStack)
        {
            //Primero se crea una copia del Stack origial para que no se modifique.

            int[] copia = new int[sourceStack.Count];
            sourceStack.CopyTo(copia, 0);
            Stack<int> segundaPila = new Stack<int>();

            //Con el ciclo for se cambian los valores a la posisción inversa, ya que al copiar los datos, estos se pasaron al revés.

            for (int i = copia.Length - 1; i >= 0; i--)
            {
                segundaPila.Push(copia[i]);
            }

            List<int> listaS = new List<int>(); //Lista donde se irán agregando los valores para ser comparados.
            Stack<int> result = new Stack<int>(); 
            List<int> horneado = new List<int>(); //Lista temporal de resultados en desorden.

            //Desde este momento se empieza el registro de cada dato.
            while (segundaPila.Count > 0)
            {
                int dato = segundaPila.Pop(); //Se toma el valor.

                listaS.Add(dato); //Se agrega el valor al la lista creada previamente.

                int mayorDato = dato; //Aquí inicia el dato mayor como el dato ya tomado.

                for (int i = 0; i < listaS.Count; i++)
                {
                    if (listaS[i] > mayorDato) //Se compara el dato.
                    {
                        mayorDato = listaS[i]; //Si cumple la condición pasa a ser el dato mayor.
                    }

                }
                if (mayorDato == dato) horneado.Add(-1);  //Si son iguales se digita un -1 en la lista temporal de resultados.
                else horneado.Add(mayorDato); //Si no, se digita el dato mayor.
            }
            for (int i = horneado.Count - 1; i >= 0; i--)
            {
                result.Push(horneado[i]); //Aquí es donde se organizan los datos a la inversa al pasarlos al resultado.
            }

            return result;

        }

        internal static Dictionary<int, EValueType> FillDictionaryFromSource(int[] sourceArr)
        {
            Dictionary<int, EValueType> result = new Dictionary<int, EValueType>();

            //Primero se crea una copia del Stack origial para que no se modifique.

            int[] copia = new int[sourceArr.Length];
            sourceArr.CopyTo(copia, 0);

            //Se realizan las pruebas para agregar cada par correcto de llaves y su valor.

            for (int i = 0; i < copia.Length; i++)
            {
                if (copia[i] % 2 == 0)
                {
                    result.Add(copia[i], EValueType.Two); 
                }

                else if (copia[i] % 3 == 0)
                {
                    result.Add(copia[i], EValueType.Three);
                }
                else if (copia[i] % 5 == 0)
                {
                    result.Add(copia[i], EValueType.Five);
                }

                else if (copia[i] % 7 == 0)
                {
                    result.Add(copia[i], EValueType.Seven);
                }

                else
                {
                    result.Add(copia[i], EValueType.Prime); //Si no se cumplen los anteriores resulta primo.
                }
            }

            return result; 

            
        }

        internal static int CountDictionaryRegistriesWithValueType(Dictionary<int, EValueType> sourceDict, EValueType type)
        {
            int result = 0;

            //Primero se crea una copia del Stack origial para que no se modifique.

            int[] direccion = new int[sourceDict.Count];
            sourceDict.Keys.CopyTo(direccion, 0);

            //Aquí recorre todo el arreglo si el dato corresponde al type.

            for (int i = 0; i < direccion.Length; i++)
            {
                if (sourceDict[direccion[i]] == type) result++;
            }
            return result;
        }

        internal static Dictionary<int, EValueType> SortDictionaryRegistries(Dictionary<int, EValueType> sourceDict)
        {
            Dictionary<int, EValueType> result = new Dictionary<int, EValueType>();

            //Primero se crea una copia del Stack origial para que no se modifique.

            int[] direccion = new int[sourceDict.Count];
            sourceDict.Keys.CopyTo(direccion, 0);

            EValueType[] valor = new EValueType[sourceDict.Count];
            sourceDict.Values.CopyTo(valor, 0);

            //Ordenar de forma descendente el arreglo de la llaves con su respectivo valor.

            for (int i = 0; i < direccion.Length; i++)
            {
                for (int j = 0; j < direccion.Length - 1; j++)
                {
                    int siguienteDir = direccion[j + 1];
                    EValueType sigVal = valor[j + 1];

                    if (direccion[j] < siguienteDir)
                    {
                        direccion[j + 1] = direccion[j];
                        direccion[j] = siguienteDir;

                        valor[j + 1] = valor[j];
                        valor[j] = sigVal;
                    }
                }
            }

            //Se agregan al diccionario.

            for (int i = 0; i < direccion.Length; i++)
            {
                result.Add(direccion[i], valor[i]);
            }

            return result; 

            
        }

        internal static Queue<Ticket>[] ClassifyTickets(List<Ticket> sourceList)
        {
            Queue<Ticket>[] result = new Queue<Ticket>[3];

            //Primero se crea una copia del Stack origial para que no se modifique.

            Ticket[] copia = new Ticket[sourceList.Count];
            sourceList.CopyTo(copia, 0);


            
            Queue<Ticket> pago = new Queue<Ticket>();
            Queue<Ticket> suscripcion = new Queue<Ticket>();
            Queue<Ticket> cancelacion = new Queue<Ticket>();

            //Se organizan de manera en que todos los turnos queden ascendentes.

            for (int i = 0; i < copia.Length; i++)
            {
                for (int j = 0; j < copia.Length - 1; j++)
                {
                    int siguienteTur = copia[j + 1].Turn;
                    Ticket siguienteTic = copia[j + 1];

                    if (copia[j].Turn > siguienteTur)
                    {
                        copia[j + 1] = copia[j];
                        copia[j] = siguienteTic;
                    }
                }
            }

            //Ya organizados se dan los turnos.

            for (int i = 0; i < copia.Length; i++)
            {
                
                if (copia[i].RequestType == Ticket.ERequestType.Payment) pago.Enqueue(copia[i]);
                else if (copia[i].RequestType == Ticket.ERequestType.Subscription) suscripcion.Enqueue(copia[i]);
                else if (copia[i].RequestType == Ticket.ERequestType.Cancellation) cancelacion.Enqueue(copia[i]);
            }

            //Se guardan en el orden.

            result[0] = pago;
            result[1] = suscripcion;
            result[2] = cancelacion;

            return result; 


        }

        internal static bool AddNewTicket(Queue<Ticket> targetQueue, Ticket ticket)
        {
            bool result = false;

            

            Ticket.ERequestType tipoTicket = targetQueue.Peek().RequestType;

            if (ticket.RequestType == tipoTicket)
            {
                if (ticket.Turn > 0 && ticket.Turn < 100)
                {
                    result = true;
                }
                    
            }
            
            return result;
        }        
    }
}