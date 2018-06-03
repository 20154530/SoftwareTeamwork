using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareTeamwork {

    public class FluxTrendGroup {
        public FluxInfo[] FluxInfos { get; set; }
        public double[] ActrualData { get; set; }
        public double[] VTicks { get; set; }

        public FluxTrendGroup() {
            FluxInfos = new FluxInfo[7] {
               new FluxInfo(),
               new FluxInfo(),
               new FluxInfo(),
               new FluxInfo(),
               new FluxInfo(),
               new FluxInfo(),
               new FluxInfo(),
            };
            ActrualData = new double[7];
            VTicks = new double[6];
        }

        public void GetFluxData() {
            GetSub();
            TransformToNode();
        }

        public override string ToString() {
            string temp = "";
            foreach(FluxInfo fi in FluxInfos) {
                temp += fi.ToString() + "\n";
            }
            return temp;
        }

        private void GetSub() {
            double d = 0.0;
            for (int i = 6; i >= 0; i--) {
                if (i > 0)
                    d = FluxInfos[i - 1].FluxData;
                else
                    d = 0;
                FluxInfos[i].FluxData -= d;
                if (FluxInfos[i].FluxData <= 0)
                    FluxInfos[i].FluxData = 0;
            }
            FluxInfos[0].FluxData = 0.0;
           
        }

        private void TransformToNode() {
            double est = 1;
            for (int i = 0; i < 7; i++) {
                ActrualData[i] = FluxInfos[i].FluxData;//备份原有数据
                est = FluxInfos[i].FluxData > est ? FluxInfos[i].FluxData : est;
            }//找最大流量

            est = est * 1.2;//画刻度
            for (int i = 0; i < 6; i++)
                VTicks[i] = est / 6 * (i + 1);

            //计算点的位置
            foreach (FluxInfo f in FluxInfos)
                f.FluxData = 96 - (f.FluxData / est) * 96;

        }
    }
}
