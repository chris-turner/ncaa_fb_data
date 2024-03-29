﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NCAA_FB_Data
{
    class Program
    {
        static void Main(string[] args)
        {
            getGameAndOddsData();
            
        }

        public static void getGameAndOddsData()
        {
            ChromeDriver driver = new ChromeDriver("C:\\Users\\Chris\\Documents\\");

            driver.Navigate().GoToUrl("http://www.espn.com/college-football/lines");

            IWebElement weekHeaderElem = driver.FindElementByXPath("//div[@class='mod-content']//child::h1");

            string weekHeaderText = weekHeaderElem.Text;

            int week = Convert.ToInt32(weekHeaderText.Substring(weekHeaderText.IndexOf("Week ") + 5));

            ReadOnlyCollection<IWebElement> gameElements = driver.FindElementsByXPath("//tr[@class='stathead']//child::td");

            List<String> gamesStrings = new List<string>();
            foreach (IWebElement gameElement in gameElements)
            {
                gamesStrings.Add(gameElement.Text);
            }


            List<Game> games = new List<Game>();

            foreach (string gameStr in gamesStrings)
            {
                Game game = new Game();
                game.awayTeam = gameStr.Split(" at ")[0];
                string newGameStr = gameStr.Split(" at ")[1];
                game.homeTeam = newGameStr.Split(" - ")[0];
                string newGameStr2 = newGameStr.Split(" - ")[1];
                string dateStr = newGameStr2.Substring(newGameStr2.IndexOf(",") + 1).Trim();
                string month = dateStr.Substring(0, dateStr.IndexOf(" "));
                Dictionary<String, String> months = new Dictionary<string, string>();
                months.Add("Jan", "1");
                months.Add("Feb", "2");
                months.Add("Mar", "3");
                months.Add("Apr", "4");
                months.Add("May", "5");
                months.Add("Jun", "6");
                months.Add("Jul", "7");
                months.Add("Aug", "8");
                months.Add("Sep", "9");
                months.Add("Oct", "10");
                months.Add("Nov", "11");
                months.Add("Dec", "12");

                string dayNum = dateStr.Substring(dateStr.IndexOf(" ") + 1, dateStr.IndexOf(",") - dateStr.IndexOf(" ") - 1);
                string time = dateStr.Substring(dateStr.IndexOf(",") + 2).Replace("ET", "");
                string finalDate = $"{months[month]}/{dayNum}/{DateTime.Now.Year} {time}";

                game.gameDateTime = Convert.ToDateTime(finalDate);
                game.gameWeek = week;
                games.Add(game);
            }

            int oddRowCount = driver.FindElementsByXPath("//tr[@class='oddrow']").Count;
            int evenRowCount = driver.FindElementsByXPath("//tr[@class='evenrow']").Count;
            ReadOnlyCollection<IWebElement> oddsElementsOdd = driver.FindElementsByClassName("oddrow") ;
            ReadOnlyCollection<IWebElement> oddsElementsEven = driver.FindElementsByClassName("evenrow");

            List<BettingInfo> bettingInfo = new List<BettingInfo>();

            for(int i = 1; i <= oddRowCount; i++)
            {
                BettingInfo bi = new BettingInfo();
                IWebElement spreads = driver.FindElementByXPath($"//tr[@class='oddrow']//child::td[2]//child::td[{i}]");

                //el.FindElement(By.XPath("//child::td[2]//child::td"));
                //bi.sportsbook = bettingInfoEl.Text;
                string spreadStr = spreads.Text;
                string awaySpread = spreadStr.Substring(0, spreadStr.IndexOf("\r"));
                string homeSpread = spreadStr.Substring(spreadStr.IndexOf("\n") + 1);

                bi.awayTeamSpread = Decimal.Parse(awaySpread);
                bi.homeTeamSpread = Decimal.Parse(homeSpread);

                bettingInfo.Add(bi);
            }

            foreach (IWebElement el in oddsElementsEven)
            {
                BettingInfo bi = new BettingInfo();
                IWebElement bettingInfoEl = el.FindElement(By.TagName("td"));
                IWebElement spreads = el.FindElement(By.XPath("//child::td[2]//child::td"));
                bi.sportsbook = bettingInfoEl.Text;
                string test = spreads.Text;
                bettingInfo.Add(bi);
            }

            driver.Close();
            driver.Dispose();
        }
    }
}
