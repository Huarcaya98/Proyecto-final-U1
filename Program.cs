using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Sistema de compra y pago");

        //1. Arreglo Productos
        int[] catIds = {1,2,3,4,5,6,7,8,9,10};
        string[]  catNombres ={"Aceite","Arroz","Azucar","Fideos","Leche","Atun","Cafe","Huevos","Lentejas","Papel"};
        double[] catPrecios ={120.50, 130.00, 95.50, 62.00, 72.00, 250.00, 98.00, 125.00, 180.00, 100.00};

        //2. Arreglo Carrito
        int[] carIds = new int[50];
        string[] carNombres = new string[50];
        int[] carCantidades = new int[50];
        double[] carSubtotales = new double[50];
        int totalItems = 0;

        bool flag = true;

        while(flag)
        {
            Console.WriteLine("--- SISTEMA DE VENTAS ---");
            Console.WriteLine("1. Ver Catálogo y Comprar");
            Console.WriteLine("2. Ver Carrito");
            Console.WriteLine("3. Eliminar Producto del Carrito");
            Console.WriteLine("4. Pagar y Generar Voucher");
            Console.Write("Seleccione una opción: ");   

            int option = int.Parse(Console.ReadLine());

            switch(option)
            {
                case 1: // COMPRAR
                        Console.Clear();
                        Console.WriteLine("=== CATÁLOGO DISPONIBLE ===");
                        Console.WriteLine("{0,-5} {1,-15} {2,-10}", "ID", "Producto", "Precio");
                        for (int i = 0; i < 10; i++)
                        {
                            Console.WriteLine($"{catIds[i],-5} {catNombres[i],-15} S/ {catPrecios[i]:N2}");
                        }

                        Console.Write("\nIngrese el ID del producto: ");
                        int idSel = int.Parse(Console.ReadLine());
                        bool encontrado = false;

                        for (int i = 0; i < 10; i++)
                        {
                            if (catIds[i] == idSel)
                            {
                                Console.Write($"¿Cuánta cantidad de {catNombres[i]} desea?: ");
                                int cant = int.Parse(Console.ReadLine());

                                carIds[totalItems] = catIds[i];
                                carNombres[totalItems] = catNombres[i];
                                carCantidades[totalItems] = cant;
                                carSubtotales[totalItems] = catPrecios[i] * cant;

                                totalItems++;
                                encontrado = true;
                                Console.WriteLine("\n¡Producto añadido al carrito!");
                                break;
                            }
                        }
                        if (!encontrado) Console.WriteLine("ID no válido.");
                        Console.WriteLine("\nPresione una tecla para continuar...");
                        Console.ReadKey();
                        break;

                    case 2: // VER CARRITO
                        Console.Clear();
                        MostrarCarrito(carIds, carNombres, carCantidades, carSubtotales, totalItems);
                        Console.WriteLine("\nPresione una tecla para volver...");
                        Console.ReadKey();
                        break;

                    case 3: // ELIMINAR CON VISTA PREVIA DEL CARRITO
                        Console.Clear();
                        if (totalItems == 0)
                        {
                            Console.WriteLine("El carrito está vacío. No hay nada que eliminar.");
                        }
                        else
                        {
                            // Mostramos el carrito para que el usuario sepa qué IDs hay
                            Console.WriteLine("=== SELECCIONE EL PRODUCTO A ELIMINAR ===");
                            MostrarCarrito(carIds, carNombres, carCantidades, carSubtotales, totalItems);
                            
                            Console.Write("\nIngrese el ID del producto que desea quitar: ");
                            int idElim = int.Parse(Console.ReadLine());
                            bool eliminado = false;

                            for (int i = 0; i < totalItems; i++)
                            {
                                if (carIds[i] == idElim)
                                {
                                    // Algoritmo de desplazamiento hacia la izquierda
                                    for (int j = i; j < totalItems - 1; j++)
                                    {
                                        carIds[j] = carIds[j + 1];
                                        carNombres[j] = carNombres[j + 1];
                                        carCantidades[j] = carCantidades[j + 1];
                                        carSubtotales[j] = carSubtotales[j + 1];
                                    }
                                    totalItems--; // Reducimos el contador global
                                    eliminado = true;
                                    Console.WriteLine("\nProducto eliminado correctamente.");
                                    break;
                                }
                            }
                            if (!eliminado) Console.WriteLine("El ID ingresado no se encuentra en el carrito.");
                        }
                        Console.WriteLine("\nPresione una tecla para volver...");
                        Console.ReadKey();
                        break;

                    case 4: // VOUCHER FINAL
                        Console.Clear();
                        if (totalItems == 0)
                        {
                            Console.WriteLine("El carrito está vacío.");
                            Console.ReadKey();
                        }
                        else
                        {
                            double subtotalFinal = 0;
                            Console.WriteLine("========================================");
                            Console.WriteLine("          VOUCHER ELECTRÓNICO           ");
                            Console.WriteLine("========================================");
                            for (int i = 0; i < totalItems; i++)
                            {
                                Console.WriteLine($"{carNombres[i],-15} x{carCantidades[i],-5} S/ {carSubtotales[i]:N2}");
                                subtotalFinal += carSubtotales[i];
                            }
                            double igv = subtotalFinal * 0.18;
                            Console.WriteLine("----------------------------------------");
                            Console.WriteLine($"SUBTOTAL:         S/ {subtotalFinal:N2}");
                            Console.WriteLine($"IGV (18%):        S/ {igv:N2}");
                            Console.WriteLine($"TOTAL A PAGAR:    S/ {subtotalFinal + igv:N2}");
                            Console.WriteLine("========================================");
                            Console.WriteLine("   ¡Gracias por su compra!");
                            Console.WriteLine("========================================");
                            flag = false; // Finaliza el ciclo
                            Console.ReadKey();
                        }
                        break;

                    case 5:
                        flag = false;
                        break;
                }
            }
        }

        // Método auxiliar para no repetir código al mostrar el carrito
        static void MostrarCarrito(int[] ids, string[] nombres, int[] cants, double[] subs, int total)
        {
            if (total == 0) Console.WriteLine("Carrito vacío.");
            else
            {
                Console.WriteLine("{0,-5} {1,-15} {2,-8} {3,-10}", "ID", "Producto", "Cant.", "Subtotal");
                for (int i = 0; i < total; i++)
                {
                    Console.WriteLine($"{ids[i],-5} {nombres[i],-15} {cants[i],-8} S/ {subs[i]:N2}");
                }
            }


        }



    }
