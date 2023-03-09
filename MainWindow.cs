using ClientViTrader.DTOs;
using LiveChartsCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.Defaults;
using System.Collections.ObjectModel;
using WebClient;
using ClientViTrader.Utils;
using System.Text.Json;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace ClientViTrader
{
    public partial class MainWindow : Form
    {
        private const string coingeckoURL = "https://api.coingecko.com/api/v3";
        private readonly UserDTO user;
        private readonly string serverURL;
        private bool tradingTabInitializaed = false, walletTabInitialized = false, accountTabInitialized = false;

        public MainWindow(UserDTO user, string serverURL)
        {
            InitializeComponent();
            this.user = user;
            this.serverURL = serverURL;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            InitializeTradingTab();
        }

        private void mainWindowTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mainWindowTabs.SelectedIndex == 1 && !walletTabInitialized)
            {
                InitializeWalletTab();
            }

            if (mainWindowTabs.SelectedIndex == 2 && !accountTabInitialized)
            {
                InitializeAccountTab();
            }
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        #region SERVER_COMM

        private async Task<List<CryptoDTO>> GetCryptos()
        {
            RestClient client = new(HTTP_VERB.GET, serverURL + "/cryptos");
            RestResponse<string> response = await client.MakeRequestAsyncText();

            if (response.Code != System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show(response.Response, "Error", MessageBoxButtons.OK);
                return new List<CryptoDTO>();
            }

            return Util.Deserialize<List<CryptoDTO>>(response.Response);
        }

        private async Task<List<IndicatorDTO>> GetIndicators()
        {
            RestClient client = new(HTTP_VERB.GET, serverURL + "/indicators");
            RestResponse<string> response = await client.MakeRequestAsyncText();

            if (response.Code != System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show(response.Response, "Error", MessageBoxButtons.OK);
                return new List<IndicatorDTO>();
            }

            return Util.Deserialize<List<IndicatorDTO>>(response.Response);
        }

        private async Task<List<PositionDTO>> GetPositions()
        {
            RestClient client = new(HTTP_VERB.GET, serverURL + "/positions?userId=" + user.id);
            client.SetHeader("Authorization", Util.GetAuthHeaderValue(user.username, user.password));
            RestResponse<string> response = await client.MakeRequestAsyncText();

            if (response.Code != System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show(response.Response, "Error", MessageBoxButtons.OK);
                return new List<PositionDTO>();
            }

            return Util.Deserialize<List<PositionDTO>>(response.Response);
        }

        private async Task<List<TradeDTO>> GetTrades()
        {
            RestClient client = new(HTTP_VERB.GET, serverURL + "/trades?userId=" + user.id);
            client.SetHeader("Authorization", Util.GetAuthHeaderValue(user.username, user.password));
            RestResponse<string> response = await client.MakeRequestAsyncText();

            if (response.Code != System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show(response.Response, "Error", MessageBoxButtons.OK);
                return new List<TradeDTO>();
            }

            return Util.Deserialize<List<TradeDTO>>(response.Response);
        }

        private async Task<List<StrategyDTO>> GetStrategies()
        {
            RestClient client = new(HTTP_VERB.GET, serverURL + "/strategies?userId=" + user.id);
            client.SetHeader("Authorization", Util.GetAuthHeaderValue(user.username, user.password));
            RestResponse<string> response = await client.MakeRequestAsyncText();

            if (response.Code != System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show(response.Response, "Error", MessageBoxButtons.OK);
                return new List<StrategyDTO>();
            }

            return Util.Deserialize<List<StrategyDTO>>(response.Response);
        }

        #endregion

        #region TRADING_TAB

        private async void InitializeTradingTab()
        {
            List<CryptoDTO> cryptos = await GetCryptos();
            List<PositionDTO> positions = await GetPositions();

            InitializeTimeframes();
            InitializeCryptos(cryptos);

            UpdateAvailableFunds(positions);
            LoadPriceChart();
            UpdatePrice();

            tradingTabInitializaed = true;
        }

        private async void LoadPriceChart()
        {
            string crypto = cryptoComboBox.Text;
            int timeframe = int.Parse(timeframeComboBox.Text.ToString().Split(' ')[0]);

            string url = coingeckoURL + "/coins/" + crypto.ToLower() + "/ohlc?vs_currency=usd&days=" + timeframe;

            RestClient client = new(HTTP_VERB.GET, url);
            RestResponse<string> response = await client.MakeRequestAsyncText();

            if (response.Code != System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show(response.Response, "Error", MessageBoxButtons.OK);
                return;
            }

            OHLCData priceData = new(JsonSerializer.Deserialize<List<double[]>>(response.Response));

            SKColor gridColor = new SKColor(48, 48, 49);
            SKColor labelsColor = new SKColor(200, 200, 200);
            Axis[] xAxis = new Axis[]
            {
                new Axis
                {
                    LabelsRotation = 15,
                    SeparatorsPaint = new SolidColorPaint(gridColor) { StrokeThickness = 1 },
                    LabelsPaint = new SolidColorPaint(labelsColor),
                }
            };

            Axis[] yAxis = new Axis[]
            {
                new Axis
                {
                    SeparatorsPaint = new SolidColorPaint(gridColor) { StrokeThickness = 1 },
                    LabelsPaint = new SolidColorPaint(labelsColor)
                }
            };

            if (timeframe == 1)
            {
                xAxis[0].Labeler = value => new DateTime((long)value).ToString("d.M h:mm");
                xAxis[0].UnitWidth = TimeSpan.FromMinutes(20).Ticks;
            }
            else if (timeframe == 7)
            {
                xAxis[0].Labeler = value => new DateTime((long)value).ToString("d.M h");
                xAxis[0].UnitWidth = TimeSpan.FromHours(2.7).Ticks;
            }
            else if (timeframe == 14)
            {
                xAxis[0].Labeler = value => new DateTime((long)value).ToString("d.M h");
                xAxis[0].UnitWidth = TimeSpan.FromHours(2).Ticks;
            }
            else if (timeframe == 30)
            {
                xAxis[0].Labeler = value => new DateTime((long)value).ToString("d.M");
                xAxis[0].UnitWidth = TimeSpan.FromHours(1).Ticks;
            }
            else if (timeframe == 180)
            {
                xAxis[0].Labeler = value => new DateTime((long)value).ToString("d.M");
                xAxis[0].UnitWidth = TimeSpan.FromDays(2.5).Ticks;
            }
            else
            {
                xAxis[0].Labeler = value => new DateTime((long)value).ToString("d.M");
                xAxis[0].UnitWidth = TimeSpan.FromDays(1.7).Ticks;
            }

            var candleChartSeries = new CandlesticksSeries<FinancialPoint>()
            {
                Name = crypto
            };

            ISeries[] series = new ISeries[] 
            {
                candleChartSeries 
            };

            var observableCollection = new ObservableCollection<FinancialPoint>();

            for (int i = 0; i < priceData.Count; i++)
            {
                if (i != 0)
                {
                    priceData[i].Open = priceData[i - 1].Close;
                }

                observableCollection.Add(priceData[i]);
            }

            series[0].Values = observableCollection;
            cartesianChart.Series = series;
            cartesianChart.XAxes = xAxis;
            cartesianChart.YAxes = yAxis;
            cartesianChart.ZoomMode = LiveChartsCore.Measure.ZoomAndPanMode.X;
            cartesianChart.TooltipFindingStrategy = LiveChartsCore.Measure.TooltipFindingStrategy.CompareAll;
            
            cartesianChart.Draw();
        }

        private void InitializeTimeframes()
        {
            string[] timeFrames = new string[]
            {
                "1 day (30min)", "7 days (4hr)", "14 days (4hr)", "30 days (4hr)", "180 days (4d)", "365 days (4d)"
            };

            timeframeComboBox.DataSource = timeFrames;
        }

        private void InitializeCryptos(List<CryptoDTO> cryptos)
        {
            cryptos = cryptos.Where(c => c.name != "Tether").ToList();

            cryptoComboBox.DataSource = cryptos;
            cryptoComboBox.DisplayMember = "name";
            cryptoComboBox.ValueMember = "name";
        }

        private void UpdateAvailableFunds(List<PositionDTO> positions)
        {
            PositionDTO usdPosition = positions.Find(p => p.cryptoId == 1);

            if (usdPosition is null)
            {
                MessageBox.Show("No USD position found.", "Error", MessageBoxButtons.OK);
                availableFundsLabel.Text = "0$";
                return;
            }

            availableFundsLabel.Text = usdPosition.amount.ToString("0.##") + "$";
        }

        private async void UpdatePrice()
        {
            string cryptoName = cryptoComboBox.Text;

            RestClient client = new(HTTP_VERB.GET, coingeckoURL + "/simple/price?ids=" + cryptoName + "&vs_currencies=usd");
            RestResponse<string> response = await client.MakeRequestAsyncText();

            if (response.Code != System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show(response.Response, "Error", MessageBoxButtons.OK);
                return;
            }

            using (var obj = JsonDocument.Parse(response.Response))
            {
                JsonElement root = obj.RootElement;
                JsonElement coin = root.GetProperty(cryptoName.ToLower());
                var price = coin.GetProperty("usd").GetDecimal();
                priceLabel.Text = price.ToString() + "$";
            }
        }

        private void cryptoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tradingTabInitializaed)
            {
                LoadPriceChart();
                UpdatePrice();
            }
        }

        private void timeframeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tradingTabInitializaed)
            {
                LoadPriceChart();
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            if (tradingTabInitializaed)
            {
                UpdatePrice();
            }
        }

        private async void buyButton_Click(object sender, EventArgs e)
        {
            CryptoDTO crypto = cryptoComboBox.SelectedItem as CryptoDTO;
            BuyForm buyForm = new(user, crypto, serverURL, coingeckoURL);

            if (buyForm.ShowDialog() == DialogResult.OK)
            {
                UpdateAvailableFunds(await GetPositions());
                InitializeWalletTab();
            }
        }

        private async void sellButton_Click(object sender, EventArgs e)
        {
            CryptoDTO crypto = cryptoComboBox.SelectedItem as CryptoDTO;
            SellForm sellForm = new(user, crypto, serverURL, coingeckoURL);

            if (sellForm.ShowDialog() == DialogResult.OK)
            {
                UpdateAvailableFunds(await GetPositions());
                InitializeWalletTab();
            }
        }

        #endregion

        #region WALLET_TAB

        private async void InitializeWalletTab()
        {
            List<PositionDTO> positions = await GetPositions();
            List<CryptoDTO> cryptos = await GetCryptos();
            List<TradeDTO> trades = await GetTrades();
            positions = await LoadPositionsData(positions, cryptos);
            trades = LoadTradesData(trades, cryptos);

            LoadPositionsChart(positions);
            LoadPositionsList(positions);
            LoadTradesList(trades);
            walletTabInitialized = true;
        }

        private async Task<List<PositionDTO>> LoadPositionsData(List<PositionDTO> positions, List<CryptoDTO> cryptos)
        {
            StringBuilder builder = new();

            foreach (var pos in positions)
            {
                pos.cryptoName = cryptos.Find(c => c.id == pos.cryptoId).name;
                builder.Append(pos.cryptoName);

                if (pos != positions.Last())
                {
                    builder.Append(',');
                }
            }

            string request = coingeckoURL + "/simple/price?ids=" + builder.ToString() + "&vs_currencies=usd";
            RestClient client = new(HTTP_VERB.GET, request);
            RestResponse<string> response = await client.MakeRequestAsyncText();

            if (response.Code != System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show(response.Response, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                var values = new Dictionary<string, decimal>();

                using (var doc = JsonDocument.Parse(response.Response))
                {
                    foreach (var el in doc.RootElement.EnumerateObject())
                    {
                        string cryptoName = el.Name;
                        var coin = JsonSerializer.Deserialize<Dictionary<string, decimal>>(el.Value.ToString());
                        values[cryptoName] = coin.Values.First();
                    }
                }

                foreach (var pos in positions)
                {
                    pos.value = pos.amount * values[pos.cryptoName.ToLower()];
                }
            }

            return positions;
        }

        private List<TradeDTO> LoadTradesData(List<TradeDTO> trades, List<CryptoDTO> cryptos)
        {
            foreach (var trade in trades)
            {
                trade.tradeType = trade.tradeTypeId == 1 ? "BUY" : "SELL";
                trade.cryptoName = cryptos.Find(c => c.id == trade.cryptoId).name;
            }

            return trades;
        }

        private void LoadPositionsChart(List<PositionDTO> positions)
        {
            portfolioValueLabel.Text = "Portfolio value: " + 
                positions.Aggregate(0M, (t, n) => t + n.value).ToString("0.##") + "$";

            ISeries[] series = new ISeries[positions.Count];
            for (int i = 0; i < positions.Count; i++)
            {
                series[i] = new PieSeries<decimal>
                {
                    Values = new List<decimal> 
                    { Math.Truncate(100 * positions[i].value) / 100 }, InnerRadius = 50,
                    Name = positions[i].cryptoName
                };
            }

            pieChart.Series = series;
            pieChart.Draw();
        }

        private void LoadPositionsList(List<PositionDTO> positions)
        {
            positionsListView.Items.Clear();

            foreach (var pos in positions)
            {
                var row = new string[] { pos.cryptoName, pos.amount.ToString("0.####"), pos.value.ToString("0.####") };
                var viewItem = new ListViewItem(row)
                {
                    Tag = pos
                };
                positionsListView.Items.Add(viewItem);
            }
        }

        private void LoadTradesList(List<TradeDTO> trades)
        {
            tradesListView.Items.Clear();

            foreach (var trade in trades)
            {
                var row = new string[] { trade.tradeType, trade.cryptoName, trade.amount.ToString("0.####"), trade.tradeTime.ToString("yyyy/MM/dd HH:mm") };
                var viewItem = new ListViewItem(row)
                {
                    Tag = trade
                };
                tradesListView.Items.Add(viewItem);
            }
        }

        private void positionsListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            List<PositionDTO> positions = new();
            foreach (ListViewItem item in positionsListView.Items)
            {
                positions.Add((PositionDTO)item.Tag);
            }

            if (e.Column == 0)
            {
                positions.Sort((p1, p2) => p2.cryptoName.CompareTo(p1.cryptoName));
            }
            else if (e.Column == 1)
            {
                positions.Sort((p1, p2) => p2.amount.CompareTo(p1.amount));
            }
            else
            {
                positions.Sort((p1, p2) => p2.value.CompareTo(p1.value));
            }

            LoadPositionsList(positions);
        }

        private void tradesListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            List<TradeDTO> trades = new();
            foreach (ListViewItem item in tradesListView.Items)
            {
                trades.Add(item.Tag as TradeDTO);
            }

            if (e.Column == 0)
            {
                trades.Sort((t1, t2) => t2.tradeType.CompareTo(t1.tradeType));
            }
            else if (e.Column == 1)
            {
                trades.Sort((t1, t2) => t2.cryptoName.CompareTo(t1.cryptoName));
            }
            else if (e.Column == 2)
            {
                trades.Sort((t1, t2) => t2.amount.CompareTo(t1.amount));
            }
            else
            {
                trades.Sort((t1, t2) => t2.tradeTime.CompareTo(t1.tradeTime));
            }

            LoadTradesList(trades);
        }

        private void positionsListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var focusedItem = positionsListView.FocusedItem;
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                {
                    contextMenuStrip1.Show(positionsListView, Cursor.Position);
                }
            }
        }

        private void tradesListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var focusedItem = tradesListView.FocusedItem;
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                {
                    contextMenuStrip2.Show(tradesListView, Cursor.Position);
                }
            }
        }

        private async void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var focusedItem = positionsListView.FocusedItem;
            PositionDTO position = focusedItem.Tag as PositionDTO;
            UpdatePositionForm updateForm = new(user, position, serverURL);

            if (updateForm.ShowDialog() == DialogResult.OK)
            {
                InitializeWalletTab();
                UpdateAvailableFunds(await GetPositions());
            }
        }

        private async void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var focusedItem = positionsListView.FocusedItem;
            if (focusedItem is null) return;
            PositionDTO position = focusedItem.Tag as PositionDTO;

            RestClient client = new(HTTP_VERB.DELETE, serverURL + "/position?id=" + position.id + "&userId=" + user.id);
            client.SetHeader("Authorization", Util.GetAuthHeaderValue(user.username, user.password));
            RestResponse<string> response = await client.MakeRequestAsyncText();

            if (response.Code != System.Net.HttpStatusCode.NoContent)
            {
                MessageBox.Show(response.Response, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                InitializeWalletTab();
                MessageBox.Show("Position deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var focusedItem = tradesListView.FocusedItem;
            if (focusedItem is null) return;
            TradeDTO trade = focusedItem.Tag as TradeDTO;

            RestClient client = new(HTTP_VERB.DELETE, serverURL + "/trade?id=" + trade.id + "&userId=" + user.id);
            client.SetHeader("Authorization", Util.GetAuthHeaderValue(user.username, user.password));
            RestResponse<string> response = await client.MakeRequestAsyncText();

            if (response.Code != System.Net.HttpStatusCode.NoContent)
            {
                MessageBox.Show(response.Response, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                tradesListView.Items.Remove(focusedItem);
                MessageBox.Show("Trade deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region ACCOUNT_TAB

        StrategyDTO loadedStrategy;

        private async void InitializeAccountTab()
        {
            List<IndicatorDTO> indicators = await GetIndicators();
            List<StrategyDTO> strategies = await GetStrategies();

            LoadAccountInfo();
            InitializeIndicators(indicators);
            
            if (strategies.Count != 0)
            {
                loadedStrategy = strategies[0];
                loadedStrategy = await LoadTriggerData(loadedStrategy);

                LoadStrategyList(strategies);
                LoadStrategy(loadedStrategy);
            }

            accountTabInitialized = true;
        }

        private async Task<StrategyDTO> LoadTriggerData(StrategyDTO strategy)
        {
            List<IndicatorDTO> indicators = await GetIndicators();

            foreach (var trigger in strategy.triggers)
            {
                trigger.indicatorName = indicators.Find(i => i.id == trigger.indicatorId).name;
                trigger.typeName = trigger.triggerTypeId == 1 ? "UNDER" : "OVER";
            }

            return strategy;
        }

        private void LoadAccountInfo()
        {
            usernameLabel.Text = user.username;
            emailLabel.Text = user.email;
            timeCreatedLabel.Text = user.dateCreated.ToString("yyyy/MM/dd");
        }

        private void LoadStrategy(StrategyDTO strategy)
        {
            strategyNameTextBox.Text = strategy.name;

            if (strategyListView.Items.Count != 0)
            {
                indicatorsListView.Items.Clear();

                foreach (var trigger in strategy.triggers)
                {
                    var row = new string[] { trigger.indicatorName, trigger.indicatorValue.ToString(), trigger.typeName };
                    var listItem = new ListViewItem(row);
                    listItem.Tag = trigger;

                    indicatorsListView.Items.Add(listItem);
                }
            }
        }

        private void InitializeIndicators(List<IndicatorDTO> indicators)
        {
            indicatorComboBox.DataSource = indicators;
            indicatorComboBox.DisplayMember = "name";
            indicatorComboBox.ValueMember = "name";
        }

        private void LoadStrategyList(List<StrategyDTO> strategies)
        {
            strategyListView.Items.Clear();

            foreach (var strat in strategies)
            {
                var row = new string[] { strat.name };
                var listItem = new ListViewItem(row);
                listItem.Tag = strat;

                strategyListView.Items.Add(listItem);
            }
        }

        private async void saveButton_Click(object sender, EventArgs e)
        {
            if (loadedStrategy is null)
            {
                MessageBox.Show("No strategy loaded. Use Save New instead.", "Save Strategy", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            strategyNameTextBox_Validated(sender, e);

            if (!strategyNameError.GetError(strategyNameTextBox).Equals(""))
                return;

            loadedStrategy.name = strategyNameTextBox.Text;
            List<TriggerDTO> triggers = new();

            foreach (ListViewItem item in indicatorsListView.Items)
            {
                TriggerDTO triggerItem = item.Tag as TriggerDTO;
                triggers.Add(triggerItem);
            }
            loadedStrategy.triggers = triggers;

            RestClient client = new(HTTP_VERB.PUT, serverURL + "/strategy?id=" + loadedStrategy.id + "&userId=" + user.id, Encoding.Unicode.GetBytes(Util.Serialize(loadedStrategy)));
            client.SetHeader("Authorization", Util.GetAuthHeaderValue(user.username, user.password));

            var response = await client.MakeRequestAsyncText();
            if (response.Code != System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show(response.Response, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                loadedStrategy = Util.Deserialize<StrategyDTO>(response.Response);
                loadedStrategy = await LoadTriggerData(loadedStrategy);
                LoadStrategy(loadedStrategy);

                foreach (ListViewItem item in strategyListView.Items)
                {
                    StrategyDTO strat = item.Tag as StrategyDTO;
                    if (strat.id == loadedStrategy.id)
                    {
                        strategyListView.Items.Remove(item);

                        var row = new string[] { loadedStrategy.name };
                        var updatedItem = new ListViewItem(row);
                        updatedItem.Tag = loadedStrategy;

                        strategyListView.Items.Add(updatedItem);
                    }
                }

                MessageBox.Show("Strategy updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void saveAsButton_Click(object sender, EventArgs e)
        {
            loadedStrategy = new();

            strategyNameTextBox_Validated(sender, e);

            if (!strategyNameError.GetError(strategyNameTextBox).Equals(""))
                return;

            loadedStrategy.name = strategyNameTextBox.Text;
            List<TriggerDTO> triggers = new();

            foreach (ListViewItem item in indicatorsListView.Items)
            {
                TriggerDTO triggerItem = item.Tag as TriggerDTO;
                triggers.Add(triggerItem);
            }
            loadedStrategy.triggers = triggers;

            RestClient client = new(HTTP_VERB.POST, serverURL + "/strategy?userId=" + user.id, Encoding.Unicode.GetBytes(Util.Serialize(loadedStrategy)));
            client.SetHeader("Authorization", Util.GetAuthHeaderValue(user.username, user.password));

            var response = await client.MakeRequestAsyncText();
            if (response.Code != System.Net.HttpStatusCode.Created)
            {
                MessageBox.Show(response.Response, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                loadedStrategy = Util.Deserialize<StrategyDTO>(response.Response);
                loadedStrategy = await LoadTriggerData(loadedStrategy);
                LoadStrategy(loadedStrategy);

                var row = new string[] { loadedStrategy.name };
                var listItem = new ListViewItem(row);
                listItem.Tag = loadedStrategy;
                strategyListView.Items.Add(listItem);

                MessageBox.Show("Strategy created.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            IndicatorDTO indicator = indicatorComboBox.SelectedItem as IndicatorDTO;

            foreach (ListViewItem item in indicatorsListView.Items)
            {
                TriggerDTO triggerItem = item.Tag as TriggerDTO;

                if (indicator.name.Equals(triggerItem.indicatorName))
                {
                    MessageBox.Show("Indicator trigger already added.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            TriggerDTO trigger = new TriggerDTO
            {
                indicatorId = indicator.id,
                indicatorName = indicator.name
            };

            CreateTriggerForm form = new(trigger);

            if (form.ShowDialog() == DialogResult.Cancel)
                return;

            var row = new string[] { trigger.indicatorName, trigger.indicatorValue.ToString(), trigger.typeName };
            var listItem = new ListViewItem(row);
            listItem.Tag = trigger;

            indicatorsListView.Items.Add(listItem);
        }

        private void updateNameButton_Click(object sender, EventArgs e)
        {
            UpdateNameForm form = new(user, serverURL);

            if (form.ShowDialog() == DialogResult.OK)
            {
                usernameLabel.Text = user.username;
            }
        }

        private async void deleteAccButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure? Application will exit.", "Delete Account", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                RestClient client = new(HTTP_VERB.DELETE, serverURL + "/user?id=" + user.id);
                client.SetHeader("Authorization", Util.GetAuthHeaderValue(user.username, user.password));

                var response = await client.MakeRequestAsyncText();

                if (response.Code != System.Net.HttpStatusCode.NoContent)
                {
                    MessageBox.Show(response.Response, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Account deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
        }

        private async void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (strategyListView.FocusedItem is null)
                return;

            loadedStrategy = strategyListView.FocusedItem.Tag as StrategyDTO;
            loadedStrategy = await LoadTriggerData(loadedStrategy);
            LoadStrategy(loadedStrategy);
        }

        private void deleteTriggerMenuButton_Click(object sender, EventArgs e)
        {
            if (indicatorsListView.FocusedItem is null)
                return;

            indicatorsListView.Items.Remove(indicatorsListView.FocusedItem);
        }

        private async void deleteToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (strategyListView.FocusedItem is null)
                return;

            StrategyDTO strategy = strategyListView.FocusedItem.Tag as StrategyDTO;
            RestClient client = new(HTTP_VERB.DELETE, serverURL + "/strategy?id=" + strategy.id + "&userId=" + user.id);
            client.SetHeader("Authorization", Util.GetAuthHeaderValue(user.username, user.password));

            var response = await client.MakeRequestAsyncText();
            if (response.Code != System.Net.HttpStatusCode.NoContent)
            {
                MessageBox.Show(response.Response, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                strategyListView.Items.Remove(strategyListView.FocusedItem);
            }
        }

        private void strategyListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var focusedItem = indicatorsListView.FocusedItem;
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                {
                    triggersListMenu.Show(indicatorsListView, Cursor.Position);
                }
            }
        }

        private void indicatorsListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var focusedItem = indicatorsListView.FocusedItem;
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                {
                    triggersListMenu.Show(indicatorsListView, Cursor.Position);
                }
            }
        }

        private void strategyNameTextBox_Validated(object sender, EventArgs e)
        {
            if (strategyNameTextBox.Text.Length > 20)
            {
                strategyNameError.SetError(strategyNameTextBox, "Name can't be longer than 20 characters.");
            }
            else if (strategyNameTextBox.Text.Length < 4)
            {
                strategyNameError.SetError(strategyNameTextBox, "Name can't be shorter than 4 characters.");
            }
            else
            {
                strategyNameError.SetError(strategyNameTextBox, "");
            }
        }

        private void steepnessTextBox_Validated(object sender, EventArgs e)
        {
            string text = steepnessTextBox.Text;
            double steepness;

            try
            {
                steepness = double.Parse(text);
            }
            catch (FormatException)
            {
                steepnessTextBoxError.SetError(steepnessTextBox, "Please enter a valid number.");
                return;
            }

            if (steepness < 0)
            {
                steepnessTextBoxError.SetError(steepnessTextBox, "Minimum amount is 0.");
            }
            else
            {
                steepnessTextBoxError.SetError(steepnessTextBox, "");
            }
        }

        private void periodTextBox_Validated(object sender, EventArgs e)
        {
            string text = periodTextBox.Text;
            int period;

            try
            {
                period = int.Parse(text);
            }
            catch (FormatException)
            {
                periodTextBoxError.SetError(periodTextBox, "Please enter a valid number.");
                return;
            }

            if (period < 1)
            {
                periodTextBoxError.SetError(periodTextBox, "Minimum amount is 1.");
            }
            else
            {
                periodTextBoxError.SetError(periodTextBox, "");
            }
        }

        private async Task<PriceData> GetPriceData()
        {
            DateTime from = fromPicker.Value;
            DateTime to = toPicker.Value;

            if (to > DateTime.Now)
            {
                MessageBox.Show("Can't use future date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
            else if (from < new DateTime(2013, 1, 1))
            {
                MessageBox.Show("Can't use dates before 2013", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (from >= to)
            {
                MessageBox.Show("From date must be before to date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
            else if ((to - from).TotalDays <= 90)
            {
                MessageBox.Show("Date difference must be greater than 90 days", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }

            RestClient client = new(HTTP_VERB.GET, coingeckoURL +
                "/coins/bitcoin/market_chart/range?vs_currency=usd&from=" + ((DateTimeOffset)from.AddDays(-PriceData.RSI_PERIOD)).ToUnixTimeSeconds() +
                "&to=" + ((DateTimeOffset)to).ToUnixTimeSeconds());

            var response = await client.MakeRequestAsyncText();

            if (response.Code != System.Net.HttpStatusCode.OK)
            {
                MessageBox.Show(response.Response, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }

            return JsonSerializer.Deserialize<PriceData>(response.Response);
        }

        private async void visualizeButton_Click(object sender, EventArgs e)
        {
            PriceData data = await GetPriceData();

            if (data is null)
                return;

            VisualizeForm form = new(data);
            form.ShowDialog();
        }

        private void tradingTab_Click(object sender, EventArgs e)
        {

        }

        private async void testStrategyButton_Click(object sender, EventArgs e)
        {
            periodTextBox_Validated(sender, e);

            if (!periodTextBoxError.GetError(periodTextBox).Equals(""))
                return;

            if (steepnessCheckBox.Checked)
            {
                steepnessTextBox_Validated(sender, e);

                if (!steepnessTextBoxError.GetError(steepnessTextBox).Equals(""))
                    return;
            }
            
            List<TriggerDTO> triggers = new();
            foreach (ListViewItem item in indicatorsListView.Items)
            {
                TriggerDTO triggerItem = item.Tag as TriggerDTO;
                triggers.Add(triggerItem);
            }

            if (triggers.Count == 0)
            {
                MessageBox.Show("No indicators set", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            PriceData data = await GetPriceData();

            if (data is null)
                return;

            RunSimulation(data, triggers);
        }

        public void RunSimulation(PriceData data, List<TriggerDTO> triggers)
        {
            double steepness = 0;

            if (steepnessCheckBox.Checked)
                steepness = double.Parse(steepnessTextBox.Text);

            TriggerDTO rsiTrigger = triggers.Find(t => t.indicatorName.Equals("RSI"));
            TriggerDTO stochRsiTrigger = triggers.Find(t => t.indicatorName.Equals("StochRSI"));

            double[] rsi = null, stochRsi = null;
            
            if (rsiTrigger is not null)
                rsi = data.CalculateRsi();

            if (stochRsiTrigger is not null)
                stochRsi = data.CalculateStochasticRsi();

            double coinAmount = 0;
            double coinAmountNoStrat = 0;
            double usdAmount = 0;
            int period = int.Parse(periodTextBox.Text);

            for (int i = PriceData.RSI_PERIOD; i < data.prices.Count; i += period)
            {
                double dailyInvestment = 1000 * period;
                coinAmountNoStrat += dailyInvestment / data.prices[i][1];

                List<double> results = new();

                if (rsiTrigger is not null)
                {
                    if (rsiTrigger.triggerTypeId == 1)
                    {
                        if (steepnessCheckBox.Checked)
                        {
                            results.Add(Math.Pow(Math.Pow(100, (double)rsiTrigger.indicatorValue / rsi[i]) / 100, steepness));
                        }    
                        else
                        {
                            if (rsi[i] < (double)rsiTrigger.indicatorValue)
                                results.Add(1);
                        }    
                    }
                    else
                    {
                        if (steepnessCheckBox.Checked)
                        {
                            results.Add(Math.Pow(Math.Pow(100, rsi[i] / (double)rsiTrigger.indicatorValue) / 100, steepness));
                        }
                        else
                        {
                            if (rsi[i] > (double)rsiTrigger.indicatorValue)
                                results.Add(1);
                        }
                    }
                }

                if (stochRsiTrigger is not null)
                {
                    if (stochRsiTrigger.triggerTypeId == 1)
                    {
                        if (steepnessCheckBox.Checked)
                        {
                            results.Add(Math.Pow(Math.Pow(100, (double)stochRsiTrigger.indicatorValue / stochRsi[i]) / 100, steepness));
                        }
                        else
                        {
                            if (stochRsi[i] < (double)stochRsiTrigger.indicatorValue)
                                results.Add(1);
                        }
                    }
                    else
                    {
                        if (steepnessCheckBox.Checked)
                        {
                            results.Add(Math.Pow(Math.Pow(100, stochRsi[i] / (double)stochRsiTrigger.indicatorValue) / 100, steepness));
                        }
                        else
                        {
                            if (stochRsi[i] > (double)stochRsiTrigger.indicatorValue)
                                results.Add(1);
                        }
                    }
                }

                for (int j = 0; j < results.Count; j++)
                {
                    if (results[j] > 1)
                        results[j] = 1;
                }

                if (accRadioButton.Checked)
                {
                    usdAmount += dailyInvestment;

                    if (results.Count != 0)
                    {
                        double multiplier = results.Aggregate(0d, (t, n) => t + n) / results.Count;
                        double usdAmountToUse = usdAmount * multiplier;

                        coinAmount += usdAmountToUse / data.prices[i][1];
                        usdAmount -= usdAmountToUse;
                    }
                }
                else if (hybridRadioButton.Checked)
                {
                    if (results.Count != 0)
                    {
                        double multiplier = results.Aggregate(0d, (t, n) => t + n) / results.Count;

                        if (multiplier == 1)
                        {
                            usdAmount += dailyInvestment;
                            double usdAmountToUse = usdAmount * multiplier;

                            coinAmount += usdAmountToUse / data.prices[i][1];
                            usdAmount -= usdAmountToUse;
                        }
                        else
                        {
                            double usdAmountToUse = dailyInvestment * multiplier;
                            dailyInvestment -= usdAmountToUse;

                            coinAmount += usdAmountToUse / data.prices[i][1];
                            usdAmount += dailyInvestment;
                        }
                    }
                    else
                    {
                        usdAmount += dailyInvestment;
                    }
                }
                else if (standardStratButton.Checked)
                {
                    if (results.Count != 0)
                    {
                        double multiplier = results.Aggregate(0d, (t, n) => t + n) / results.Count;
                        double usdAmountToUse = dailyInvestment * multiplier;

                        dailyInvestment -= usdAmountToUse;
                        coinAmount += usdAmountToUse / data.prices[i][1];
                    }

                    usdAmount += dailyInvestment;
                }
            }

            double totalValueAmount = coinAmount * data.prices.Last()[1] + usdAmount;
            double totalValueAmountNoStrat = coinAmountNoStrat * data.prices.Last()[1];

            bool profitable;
            double percentDifference;

            if (totalValueAmount > totalValueAmountNoStrat)
            {
                profitable = true;
                percentDifference = (totalValueAmount - totalValueAmountNoStrat) / totalValueAmountNoStrat * 100;
            }
            else
            {
                profitable = false;
                percentDifference = (totalValueAmountNoStrat - totalValueAmount) / totalValueAmountNoStrat * 100;
            }

            resultLabel.Text = "Investing according to this strategy daily would result";
            resultLabel2.Text = "in a " + percentDifference.ToString("0.##") + "% " + (profitable ? "increase" : "decrease") + " in value over just investing a fixed amount daily.";
        }

        #endregion
    }
}
