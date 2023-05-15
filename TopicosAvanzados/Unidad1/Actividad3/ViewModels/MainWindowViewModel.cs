using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TopicosAvanzados.Unidad1.Actividad3.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        /*
    Realiza un programa que te permita jugar a "Adivina un número", juego en el cual la computadora generará un valor aleatorio entre 1 y 5000.
El jugador intentará adivinar el número generado y si no lo adivina, la computadora mostrará una pista indicando si el número a adivinar es menor o mayor
Si el jugador adivina el juego termina.
El jugador tendrá 10 oportunidades para adivinar y si termina estas oportunidades, el juego termina.
Realiza un diseño de tu agrado.
Se calificará: Diseño de la vista y funcionalidad del programa
*/
        //Numero aleatorio 1-5000
        //pista indicando si el numero dado es mayor o menor
        //si se adivina el juego termina
        //10 intentos
        public ICommand AdivinarCommand { get; set; }
        public ICommand ReiniciarCommand { get; set; }
        public int NumeroAdivinar { get; set; }
        public int Intentos { get; set; }
        public string Pista { get; set; } = "";
        private string visibilidad = "Collapsed";

        public string VisibilidadReiniciar
        {
            get { return visibilidad; }
            set
            {
                visibilidad = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VisibilidadReiniciar)));
            }
        }

        private string numerodado = "";
        //NumeroDado es el numero que el usuario nos proporciona
        public string NumeroDado
        {
            get { return numerodado; }
            set
            {
                numerodado = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumeroDado)));
            }
        }

        public MainWindowViewModel()
        {
            Iniciar();
            AdivinarCommand = new RelayCommand(Adivinar);
            ReiniciarCommand = new RelayCommand(Iniciar);
        }
        //regex para validar que el texto del numerodado sean solo numeros y no letras u otros caracteres
        Regex test = new Regex(@"^\d+$");
        private void Adivinar()
        {
            if (Intentos >= 1)
                if (test.IsMatch(NumeroDado))
                {
                    if (int.Parse(NumeroDado) == NumeroAdivinar)
                    {
                        Pista = "Ganaste";
                        VisibilidadReiniciar = "Visible";
                    }
                    else if (int.Parse(NumeroDado) > NumeroAdivinar)
                        Pista = "El número es menor";
                    else
                        Pista = "El número es mayor";
                    Intentos--;
                    PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(Intentos)));
                }
                else
                    Pista = "Favor de escribir solamente números";
            else
            {
                Pista = "Te quedaste sin intentos… pensé que este mensaje jamás saldría";
                VisibilidadReiniciar = "Visible";
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pista)));
        }
        /*Iniciar establece los valores iniciales (intentos, pista y el numero a adivinar), tambien funciona para reiniciar el juego ya 
         * que es lo mismo en si*/
        private void Iniciar()
        {
            Random r = new Random();
            NumeroAdivinar = r.Next(1, 5001);
            Intentos = 10;
            Pista = "";
            visibilidad = "Collapsed";
            numerodado = "";
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}