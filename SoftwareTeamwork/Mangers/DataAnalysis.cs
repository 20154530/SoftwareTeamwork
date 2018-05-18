using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareTeamwork {
    class DataAnalysis {

        #region FluxData
        public static double GetFluxPercent(bool update) {
            if (update)
                DataFormater.Instense.IpgwInfo = DataFormater.Instense.UpdateFlux();
            try {
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
            catch (Exception) {
                MessageService.Instence.ShowError(null, "IPGW网关未连接");
                return 0;
            }
        }

        public static double GetFluxData(bool use) {
            try {
                if (use)
                    return DataFormater.Instense.IpgwInfo.FluxData;
                else {
                    double all = Properties.Settings.Default.FluxPackage ?
                    (DataFormater.Instense.IpgwInfo.Balance - 20) * 1000 + 60000 :
                    (DataFormater.Instense.IpgwInfo.Balance - 15) * 1000 + 27000;
                    return all - DataFormater.Instense.IpgwInfo.FluxData;
                }
            }
            catch (Exception) {
                MessageService.Instence.ShowError(null, "IPGW网关未连接");
                return 0;
            }
        }

        public static FluxTrendGroup GetFluxTrendGroup() {
            FluxTrendGroup temp = new FluxTrendGroup();

            return temp;
        }

        #endregion
    }
}
