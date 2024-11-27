using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace CardGame
{
    class Program 
    {
        static void Main(string[] args)
        {
            // init list of objects
            List<Card> deck = new List<Card>();

            // append each card
            // clubs
            deck.Add(new Card("5", "clubs", 1, 5));
            deck.Add(new Card("6", "clubs", 2, 0));
            deck.Add(new Card("7", "clubs", 3, 0));
            deck.Add(new Card("8", "clubs", 4, 0));
            deck.Add(new Card("9", "clubs", 5, 0));
            deck.Add(new Card("10", "clubs", 6, 10));
            deck.Add(new Card("J", "clubs", 7, 0));
            deck.Add(new Card("Q", "clubs", 8, 0));
            deck.Add(new Card("K", "clubs", 9, 0));
            deck.Add(new Card("A", "clubs", 10, 10));

            // diamonds
            deck.Add(new Card("5", "diamonds", 1, 5));
            deck.Add(new Card("6", "diamonds", 2, 0));
            deck.Add(new Card("7", "diamonds", 3, 0));
            deck.Add(new Card("8", "diamonds", 4, 0));
            deck.Add(new Card("9", "diamonds", 5, 0));
            deck.Add(new Card("10", "diamonds", 6, 10));
            deck.Add(new Card("J", "diamonds", 7, 0));
            deck.Add(new Card("Q", "diamonds", 8, 0));
            deck.Add(new Card("K", "diamonds", 9, 0));
            deck.Add(new Card("A", "diamonds", 10, 10));

            // hearts
            deck.Add(new Card("5", "hearts", 1, 5));
            deck.Add(new Card("6", "hearts", 2, 0));
            deck.Add(new Card("7", "hearts", 3, 0));
            deck.Add(new Card("8", "hearts", 4, 0));
            deck.Add(new Card("9", "hearts", 5, 0));
            deck.Add(new Card("10", "hearts", 6, 10));
            deck.Add(new Card("J", "hearts", 7, 0));
            deck.Add(new Card("Q", "hearts", 8, 0));
            deck.Add(new Card("K", "hearts", 9, 0));
            deck.Add(new Card("A", "hearts", 10, 10));

            // spades
            deck.Add(new Card("5", "spades", 1, 5));
            deck.Add(new Card("6", "spades", 2, 0));
            deck.Add(new Card("7", "spades", 3, 0));
            deck.Add(new Card("8", "spades", 4, 0));
            deck.Add(new Card("9", "spades", 5, 0));
            deck.Add(new Card("10", "spades", 6, 10));
            deck.Add(new Card("J", "spades", 7, 0));
            deck.Add(new Card("Q", "spades", 8, 0));
            deck.Add(new Card("K", "spades", 9, 0));
            deck.Add(new Card("A", "spades", 10, 10));



            // disp deck of cards
            foreach (Card card in deck)
            { Console.WriteLine(card); }

            Console.WriteLine("\n#########################\n");

            ShuffleDeck();
            ShuffleDeck();
            ShuffleDeck();

            // disp deck of cards
            foreach (Card card in deck)
            { Console.WriteLine(card); }

            Console.WriteLine("\n#########################\n");


            List<Card> player1 = new List<Card>();
            List<Card> player2 = new List<Card>();
            List<Card> player3 = new List<Card>();
            List<Card> player4 = new List<Card>();


            for (int i = 0; i < (deck.Count); i += 4)
            {
                player1.Add(deck[i]);
                player2.Add(deck[i + 1]);
                player3.Add(deck[i + 2]);
                player4.Add(deck[i + 3]);
            }

            Console.WriteLine("\n#########################\n");

            // disp player1 hand
            foreach (Card player in player1)
            {  Console.WriteLine(player); }

            Console.WriteLine("\n#########################\n");

            // disp player1 hand
            foreach (Card player in player2)
            { Console.WriteLine(player); }

            Console.WriteLine("\n#########################\n");

            // disp player1 hand
            foreach (Card player in player3)
            { Console.WriteLine(player); }

            Console.WriteLine("\n#########################\n");

            // disp player1 hand
            foreach (Card player in player4)
            { Console.WriteLine(player); }


            // next steps
            // 1. define game logic
            //  a. bets -> bet value and trump suit decision
            //  b. play round -> high card win, or highest trump, keep track of points
            //  c. calculate ending points per team


            Console.WriteLine("\n#########################\n");

            List<Card> trick = new List<Card>();
            for (int i = 0; i < 4; i++)
            {
                trick.Add(deck[i]);
            }

            foreach (Card card in trick)
            { Console.WriteLine(card); }

            Console.WriteLine("\n#########################\n");

            Card cardwinner = TrickWinner(trick[0], trick[1], trick[2], trick[3]);
            Console.WriteLine(cardwinner);

            Console.WriteLine("\n#########################\n");
            

            //Card winner = TrickWinner(deck[1], deck[2], deck[3], deck[4]);
            //Console.WriteLine(winner);

            


            void ShuffleDeck()
            {
                int n = 0;
                while (n < deck.Count)
                {
                    Random random = new Random();
                    int randomCardIndex = random.Next(n, deck.Count);
                    Card temp = deck[randomCardIndex];
                    deck[randomCardIndex] = deck[n];
                    deck[n] = temp;
                    n++;
                }
            }


            //string BiddingRound(int player1Bid, int player2Bid, int player3Bid, int player4Bid)
            //{

            //    return player;

            //}


            // need to fix this, want to return the Card object, not the number
            Card TrickWinner(Card player1Card, Card player2Card, Card player3Card, Card player4Card)
            {
                int maxVal = trick.Max(x => x.cardFaceValue);
                Card maxCard = trick.First(x => x.cardFaceValue == maxVal);
                return maxCard;
            }

            Console.ReadKey();
        }

    }

    class Card
    {
        public string cardFace; // 5, 6, 7, 8, 9, 10, J, Q, K, A
        public string cardSuit; // clubs, diamonds, hearts, spades
        public int cardFaceValue; // 1, 2, 3, 4, 5, 6, 7, 8, 9, 10
        public int cardPointValue; // 0, 5, 10
        public Card(string cardFace, string cardSuit, int cardFaceValue, int cardPointValue)
        {
            this.cardFace = cardFace;
            this.cardSuit = cardSuit;
            this.cardFaceValue = cardFaceValue;
            this.cardPointValue = cardPointValue;
        }

        public override string ToString()
        {
            return cardFace + "("+cardFaceValue + ")" + " of " + cardSuit + " = " + cardPointValue + " points";
        }
    }
}

// Questions pour Professor sensei Marc
// 1. console app then unity?
// 2. github, milestone/versions for subsections in code??
// 3. where to place ShuffleDeck() method?
// 4. can use enum to rank card high card ranking or create a cardFaceValue field or just use cardface as high card
