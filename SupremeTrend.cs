#region Using declarations
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.SuperDom;
using NinjaTrader.Gui.Tools;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

//This namespace holds Indicators in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Indicators
{
	public class SupremeTrend : Indicator
	{
		private EMA EMA1;
		private RSI RSI1;
		private ADX ADX1;
		
		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				Description									= @"Enter the description for your new custom Indicator here.";
				Name										= "Supreme Trend";
				Calculate									= Calculate.OnBarClose;
				IsOverlay									= false;
				DisplayInDataBox							= true;
				DrawOnPricePanel							= true;
				DrawHorizontalGridLines						= true;
				DrawVerticalGridLines						= true;
				PaintPriceMarkers							= true;
				ScaleJustification							= NinjaTrader.Gui.Chart.ScaleJustification.Right;
				//Disable this property if your indicator requires custom values that cumulate with each new market data event. 
				//See Help Guide for additional information.
				IsSuspendedWhileInactive					= true;
				//AddPlot(Brushes.Black, "RSISup");
				//AddPlot(Brushes.Red, "ADXSup");
			}
			else if (State == State.Configure)
			{
			}
			else if (State == State.DataLoaded)
			{				
				EMA1				= EMA(Close, 50);
				RSI1				= RSI(Close, 3, 3);
				ADX1				= ADX(Close, 5); 
			}
		}

		protected override void OnBarUpdate()
		{
			if (BarsInProgress != 0) 
				return;

			if (CurrentBars[0] < 5)
				return;
			// set line values
			//RSISup[0] = RSI1.Avg[0];
			//ADXSup[0] = ADX1[0];
			
			// show long trades
			if ((Close[0] > EMA1[0])
				 && (RSI1.Avg[0] <= 30)
				 && (ADX1[0] > 30)
				 && IsRising(EMA1)
				)
			{
				Draw.ArrowUp(this, "LongEntry"+CurrentBar, false, 0, Low[0], Brushes.DodgerBlue);
			}
			
			// show short trades
			if ((Close[0] < EMA1[0])
				 && (RSI1.Avg[0] >= 70)
				 && (ADX1[0] > 30)
				 && IsFalling(EMA1)
				)
			{
				Draw.ArrowDown(this, "ShortEntry"+CurrentBar, false, 0, High[0], Brushes.Red);
			}
		}

		#region Properties

		[Browsable(false)]
		[XmlIgnore]
		public Series<double> RSISup
		{
			get { return Values[0]; }
		}

		[Browsable(false)]
		[XmlIgnore]
		public Series<double> ADXSup
		{
			get { return Values[1]; }
		}
		#endregion

	}
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		private SupremeTrend[] cacheSupremeTrend;
		public SupremeTrend SupremeTrend()
		{
			return SupremeTrend(Input);
		}

		public SupremeTrend SupremeTrend(ISeries<double> input)
		{
			if (cacheSupremeTrend != null)
				for (int idx = 0; idx < cacheSupremeTrend.Length; idx++)
					if (cacheSupremeTrend[idx] != null &&  cacheSupremeTrend[idx].EqualsInput(input))
						return cacheSupremeTrend[idx];
			return CacheIndicator<SupremeTrend>(new SupremeTrend(), input, ref cacheSupremeTrend);
		}
	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		public Indicators.SupremeTrend SupremeTrend()
		{
			return indicator.SupremeTrend(Input);
		}

		public Indicators.SupremeTrend SupremeTrend(ISeries<double> input )
		{
			return indicator.SupremeTrend(input);
		}
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		public Indicators.SupremeTrend SupremeTrend()
		{
			return indicator.SupremeTrend(Input);
		}

		public Indicators.SupremeTrend SupremeTrend(ISeries<double> input )
		{
			return indicator.SupremeTrend(input);
		}
	}
}

#endregion
