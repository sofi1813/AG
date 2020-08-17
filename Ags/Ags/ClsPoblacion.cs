using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ags
{
    class ClsPoblacion
    {
        private ClsIndividuo[] _individuos;
        private int _tam_poblacion;
        private double[] _fitness;
        private double populationFitness;

        public ClsPoblacion(int tam)
        {
            _tam_poblacion = tam;
            populationFitness = 0;
            _individuos = new ClsIndividuo[_tam_poblacion];
            _fitness = new double[_tam_poblacion];
        }

        public ClsIndividuo[] Poblacion
        {
            get => clonar_poblacion(_individuos);
            set => _individuos = clonar_poblacion(value);
        }

        public double[] Fitness
        {
            get => clonar_fitness(_fitness);
            set
            {
                _fitness = clonar_fitness(value);
                getPopulationFitness();
            }
        }

        public int getTam
        {
            get => _tam_poblacion;
        }

        private ClsIndividuo[] clonar_poblacion(ClsIndividuo[] poblacion)
        {
            ClsIndividuo[] clon = new ClsIndividuo[_tam_poblacion];
            for (int i = 0; i < _tam_poblacion; i++)
            {
                clon[i] = poblacion[i];
            }
            return clon;
        }

        private double[] clonar_fitness(double[] fitness)
        {
            double[] copia = new double[fitness.Length];
            for (int i = 0; i < fitness.Length; i++)
                copia[i] = fitness[i];
            return copia;
        }

        public ClsIndividuo[] generar_poblacion(ClsSuperIndividuo super_indv)
        {
            for (int i = 0; i < _tam_poblacion; i++)
            {
                _individuos[i] = new ClsIndividuo(super_indv);
            }

            return Poblacion;
        }

        public void _Evalua_Poblacion()
        {
            for (int i = 0; i < _tam_poblacion; i++)
            {
                _fitness[i] = ClsEvaluacion.fitness(_individuos[i]);
            }

            getPopulationFitness();
        }

        public int TheBest//Retorna la posicion del mejor dentro del arreglo de individuos
        {
            get => _ObtenerPosicion_TheBest();
        }

        public double TheBestFitness//retorna el mejor fitness
        {
            get => _fitness[_ObtenerPosicion_TheBest()];
        }

        private void getPopulationFitness()
        {
            for (int i = 0; i < _fitness.Length; i++)
                populationFitness += _fitness[i];
            populationFitness /= _fitness.Length;
        }

        private int _ObtenerPosicion_TheBest()
        {
            int p = 0;

            for (int i = 1; i < _fitness.Length; i++)
                if (_fitness[i] < _fitness[p])
                    p = i;

            return p;
        }

        private double _Calcular_DesviacionEstandar()
        {
            double desviacion = 0;
            double suma_distMedia = 0;
            double suma = 0;
            for(int i = 0; i < _fitness.Length; i++)
            {
                suma = _fitness[i] - populationFitness;
                suma_distMedia += Math.Pow(suma, 2);
            }

            desviacion = suma_distMedia / _fitness.Length;

            desviacion = Math.Sqrt(desviacion);

            return desviacion;
        }

        public String toString()
        {
            //           el mejor fitness                medida de dispersion        fitness de la poblacion
            return "{ " + TheBestFitness + " | " + _Calcular_DesviacionEstandar() + " | " + populationFitness +" }";
        }
    }
}
