using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareTeamwork {
    class DataAnalysis {

        #region FluxData
        public static double GetFluxPercent(bool fresh) {
            if (fresh)
                DataFormater.Instense.UpdateFlux();
            if (Properties.Settings.Default.FluxPackage) {
                double all = DataFormater.Instense.IpgwInfo.Balance - 20;
                all = all * 1000 + 60000;
                return DataFormater.Instense.IpgwInfo.FluxData / all;
            }
            else {
                double all = DataFormater.Instense.IpgwInfo.Balance - 15;
                all = all * 1000 + 27000;
                return DataFormater.Instense.IpgwInfo.FluxData / all;
            }
        }

        public static FluxTrendGroup GetFluxTrendGroup() {
            FluxTrendGroup temp = new FluxTrendGroup();

            return temp;
        }

        #endregion
    }
}
