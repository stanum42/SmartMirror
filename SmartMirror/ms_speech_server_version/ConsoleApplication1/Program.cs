using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.Synthesis;
using Microsoft.Speech.AudioFormat;
using System.IO;
using System.Net;
using NAudio.Wave;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ConsoleApplication1
{
    class Program :Form
    {
        [DllImport("user32.dll")]
        public static extern void keybd_event(uint bVk, uint bScan, long dwFlags, long dwExtraInfo);

        public static Form1 window_form = null;
        public static Thread musicThread = null;
        //public WebBrowser brow = new WebBrowser();

        public static int music;
        public static int music_playing;
        public static int document;
        public static int document_classification;
        public static int map;
        public static int movie;
        public static int movie_count;
        public static int Time;
        public static int search;
        public static int imgsearch;

        //        [STAThread]
        static void Main(string[] args)
        {
            //Process.Start(@"C:/Program Files (x86)/Google/Chrome/Application/chrome.exe -kiosk https://search.naver.com/search.naver?ie=utf8&sm=stp_hty&where=se&query=" + givenString);
            music = 0;
            document = 0;
            map = 0;
            movie = 0;
            movie_count = 0;
            music_playing = 0;
            Time = 0;
            search = 0;
            imgsearch = 0;
            document_classification = 0;
            
            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {
                foreach (InstalledVoice voice in synth.GetInstalledVoices())
                {
                    VoiceInfo info = voice.VoiceInfo;
                    string AudioFormats = "";
                    foreach (SpeechAudioFormatInfo fmt in info.SupportedAudioFormats)
                    {
                        AudioFormats += String.Format("{0}\n",
                        fmt.EncodingFormat.ToString());
                    }

                    Console.WriteLine(" Name:          " + info.Name);
                    Console.WriteLine(" Culture:       " + info.Culture);
                    Console.WriteLine(" Age:           " + info.Age);
                    Console.WriteLine(" Gender:        " + info.Gender);
                    Console.WriteLine(" Description:   " + info.Description);
                    Console.WriteLine(" ID:            " + info.Id);
                    Console.WriteLine(" Enabled:       " + voice.Enabled);
                    if (info.SupportedAudioFormats.Count != 0)
                    {
                        Console.WriteLine(" Audio formats: " + AudioFormats);
                    }
                    else
                    {
                        Console.WriteLine(" No supported audio formats found");
                    }

                    string AdditionalInfo = "";
                    foreach (string key in info.AdditionalInfo.Keys)
                    {
                        AdditionalInfo += String.Format("  {0}: {1}\n", key, info.AdditionalInfo[key]);
                    }

                    Console.WriteLine(" Additional Info - " + AdditionalInfo);
                    Console.WriteLine();
                }
            }

            foreach (RecognizerInfo ri in SpeechRecognitionEngine.InstalledRecognizers())
            {
                Console.WriteLine(ri.Id + ": " + ri.Culture);
            }

            using (SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine("SR_MS_ko-KR_TELE_11.0"))
            {
                Grammar grammar = new Grammar("computer.xml");

                recognizer.LoadGrammar(grammar);

                recognizer.SetInputToDefaultAudioDevice();
                recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);

                recognizer.RecognizeAsync(RecognizeMode.Multiple);
                window_form = new Form1();
                

                new Thread(new ThreadStart(showForm)).Start();
                new Thread(new ThreadStart(showWeb)).Start();
                while (true)
                {
                    Console.ReadLine();
                }
            }
        }

        static RecognitionState current = RecognitionState.None;

        static void exit()
        {
            music = 0;
            map = 0;
            if(document != 0)
            {
                if (musicThread != null)
                {
                    Console.Write("isAlive: ");
                    if (musicThread.IsAlive) musicThread.Abort();
                }
            }
            document = 0;
            movie = 0;
            movie_count = 0;
            Time = 0;
            search = 0;
            imgsearch = 0;
            window_form.black();
            window_form.unsetSearch();
            window_form.unsetTime();
            document = 0;
            document_classification = 0;

            

        }
        static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.Write("Input: ");
            Console.WriteLine(e.Result.Text);
            Console.WriteLine("-----------------");

            // 전체 종료를 해야함
            if (e.Result.Text == "종료"){
                exit();
            }

            if (e.Result.Text == "음악"){
                exit();
                music = 1;
                window_form.setMusicList();
            }
            if (e.Result.Text == "문서"){
                exit();
                document = 1;
                window_form.setDocumentList();
                if (musicThread != null)
                    musicThread.Abort();
                return;
            }
            if (e.Result.Text == "지도"){
                exit();
                map = 1;
                window_form.setMapList();
            }
            if (e.Result.Text == "영화")
            {
                exit();
                movie = 1;
                movie_count = 1;
                window_form.setMovieList();
            }
            if (e.Result.Text == "시간")
            {
                exit();
                Time = 1;
                window_form.setTime();
            }
            if (e.Result.Text == "검색")
            {
                exit();
                window_form.setSearchImg();
                search = 1;
                return;
            }
            if (e.Result.Text == "이미지검색")
            {
                exit();
                window_form.setImgSearchImg();
                imgsearch = 1;
                return;
            }

            //////////////////////////////////////////////////
            if( music == 1){
                
                if (e.Result.Text == "그대 바람이 되어")
                {
                    if (music_playing == 0)
                    {
                        musicThread = new Thread(() => { PlayMp3FromUrl("http://julia.snubi.org/sosal/upload/WindBeneathYourWings.mp3"); });
                        window_form.setMusic("WindBeneathYourWings");
                        musicThread.Start();
                    }
                    music_playing = 1;
                }
                if (e.Result.Text == "사랑이었다")
                {
                    if (music_playing == 0)
                    {
                        musicThread = new Thread(() => { PlayMp3FromUrl("http://julia.snubi.org/sosal/upload/ItwasLove.mp3"); });
                        window_form.setMusic("ItWasLove");
                        musicThread.Start();
                    }
                    music_playing = 1;
                }
                if (e.Result.Text == "남과 여")
                {
                    if (music_playing == 0)
                    {
                        musicThread = new Thread(() => { PlayMp3FromUrl("http://julia.snubi.org/sosal/upload/LadiesAndGentlemen.mp3"); });
                        window_form.setMusic("LadiesAndGentlemen");
                        musicThread.Start();
                    }
                    music_playing = 1;
                }
            }
            if (e.Result.Text == "음악종료")
            {
                if (musicThread != null)
                {
                    Console.Write("isAlive: ");
                    if (musicThread.IsAlive) musicThread.Abort();
                }
                music_playing = 0;
                music = 1;
            }
            if (document == 1)
            {
                if (e.Result.Text == "뉴스")
                {
                    document_classification = 1;
                    document = 2;
                    window_form.setDocument(document_classification);
                    return;
                }
                if (e.Result.Text == "소설")
                {
                    document_classification = 2;
                    document = 2;
                    window_form.setDocument(document_classification);
                    return;
                }
                if (e.Result.Text == "스포츠")
                {
                    document_classification = 3;
                    document = 2;
                    window_form.setDocument(document_classification);
                    return;
                }
                if (e.Result.Text == "뻔" || e.Result.Text == "펀" || e.Result.Text == "번")
                {
                    document_classification = 4;
                    document = 2;
                    window_form.setDocument(document_classification);
                    return;
                }
            }
            if(document == 2)
            {
                if (e.Result.Text == "재생")
                {
                    window_form.setDocumentAudio(document_classification);
                    switch (document_classification)
                    {
                        case 1:
                            musicThread = new Thread(() => { PlayMp3FromUrl("http://julia.snubi.org/sosal/upload/news.mp3"); });
                            musicThread.Start();
                            break;
                        case 2:
                            musicThread = new Thread(() => { PlayMp3FromUrl("http://julia.snubi.org/sosal/upload/novel.mp3"); });
                            musicThread.Start();
                            break;
                        case 3:
                            musicThread = new Thread(() => { PlayMp3FromUrl("http://julia.snubi.org/sosal/upload/soirts.mp3"); });
                            musicThread.Start();
                            break;
                        case 4:
                            musicThread = new Thread(() => { PlayMp3FromUrl("http://julia.snubi.org/sosal/upload/fun.mp3"); });
                            musicThread.Start();
                            break;
                        default:
                            break;
                    }
                    
                }
            }
            if ( map == 1)
            {
                if (e.Result.Text == "") window_form.setMap();
                if (e.Result.Text == "") window_form.setMap();
                if (e.Result.Text == "") window_form.setMap();
                if (e.Result.Text == "") window_form.setMap();
                if (e.Result.Text == "") window_form.setMap();
            }

            if (movie == 1)
            {
                if (e.Result.Text == "다음")
                {
                    if(movie_count == 5)
                        movie_count = 1;
                    else
                        movie_count = movie_count + 1;
                    window_form.setMovie(movie_count);
                    
                }
                
                //if (e.Result.Text == "실행") window_form.webBrowser1.Url = "";
                if (e.Result.Text == "재생")
                {
                    window_form.execMovie(movie_count);
                }
            }
            if (search == 1)
            {
                window_form.setSearch(e.Result.Text);
                return;
            }

            if (imgsearch == 1)
            {

                if (e.Result.Text == "다음")
                {
                    keybd_event(39U, 0U, 0L, 0L);
                    keybd_event(39U, 0U, 2L, 0L);
                }
                else if (e.Result.Text == "이전")
                {
                    keybd_event(37U, 0U, 0L, 0L);
                    keybd_event(37U, 0U, 2L, 0L);
                }
                else window_form.setImgSearch(e.Result.Text);
                return;
            }
        }

        private static void DoHibernation()
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.Arguments = "/h /f";
            psi.FileName = "c:\\windows\\system32\\shutdown.exe";
            //Process.Start(psi);
        }

        public static void PlayMp3FromUrl(string url)
        {
            Console.WriteLine("치킨이나 먹자");
            using (Stream ms = new MemoryStream())
            {
                using (Stream stream = WebRequest.Create(url)
                    .GetResponse().GetResponseStream())
                {
                    byte[] buffer = new byte[32768];
                    int read;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                }

                ms.Position = 0;
                using (WaveStream blockAlignedStream =
                    new BlockAlignReductionStream(
                        WaveFormatConversionStream.CreatePcmStream(
                            new Mp3FileReader(ms))))
                {
                    using (WaveOut waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
                    {
                        waveOut.Init(blockAlignedStream);
                        waveOut.Play();
                        while (waveOut.PlaybackState == PlaybackState.Playing)
                        {
                            System.Threading.Thread.Sleep(100);
                        }
                    }
                }
            }
        }

        public static void showForm()
        {
            Application.EnableVisualStyles();
            Application.Run(window_form);
        }
        public static void showWeb()
        {
            
        }

    }

    public enum RecognitionState
    {
        None,
        Question,
    }
}
