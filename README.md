# FFmpeg Batch AV Converter

FFmpeg Batch AV Converter is an ffmepg gui, a front-end for Windows and Linux [using Wine-Mono](https://github.com/eibol/ffmpeg_batch/blob/master/README_Linux_wine.md), that allows the use of the full potential of ffmpeg command line with a few mouse clicks in a convenient GUI. Among other things, you can drag and drop, see progress information, change encoding priority, pause and 
resume, and set automatic shutdown. It is good for seasoned ffmpeg users as well as beginners.

It provides unlimited single or multi-file batch encoding for almost any audio/video format. You can use any set of parameters 
and try them before starting encoding. You can manipulate and multiplex streams, batch subtitle (as track and hardcoded), 
trim, concatenate, record screen, capture M3u8 or YouTube URLs. You can also access useful multimedia file information.

You can manually save your favourite custom ffmpeg parameters, using a fancy encoding wizard. You can use relative/absolute output 
paths, automatically rename output files, overwrite them etc.

FEATURES

   - Video encoding: AV1 / H264 / H265 / NVENC / QuickSync / ProRes / VP9 / Any other video format supported by ffmpeg.
   - Audio encoding: MP3 / AAC / AC3 / FLAC / WAV / Opus / Vorbis / Any other audio format supported by ffmpeg.
   - Unlimited batch processing and automatic folder monitoring.
   - Multi-file simultaneous encoding.
   - Two pass encoding and fixed output target size.
   - Dynamic variables for ffmpeg parameters.
   - Automatic shutdown, with option to run post-encoding executables.   
   - Set encoding priority
   - Stream mapping and multiplex with jobs manager.
   - Batch stream subtitles and hardcoded (burnt) subtitles. 
   - Batch mux and demux.
   - FFmpeg wizard
   - Filter files using different criteria.
   - File multimedia info and up to 12 properties columns.
   - Trim and concatenate files
   - Batch image thumbnail extraction
   - Batch image to video creation.
   - Batch audio silence detection.
   - Batch file split and chapters creation.
   - Batch trailer/extract sample videos.
   - Batch download YouTube videos, Live YouTube events and m3u8 links.
   - Youtube-dl / yt-dlp frontend GUI for any supported URL.

Since 2017 FFmpeg Batch AV Converter has been developed as a free and open source application.

If it is useful for you, please consider supporting this work with a complimentary donation.

[Donations](https://sourceforge.net/p/ffmpeg-batch/wiki/ffmpeg-batch/)

![Buy me a coffee]([https://img.buymeacoffee.com/button-api/?text=Buy%20me%20a%20coffee&emoji=&slug=ffbatch&button_colour=FFDD00&font_colour=000000&font_family=Cookie&outline_colour=000000&coffee_colour=ffffff](https://buymeacoffee.com/ffbatch))

<img src="https://raw.githubusercontent.com/eibol/ffmpeg_batch/refs/heads/gh-pages/btc_eib.jpg" align="left">
BTC: 394svDjYkfYtakt5L4Ce3whjpToPfYGej2
Available languages:

| English | Spanish | French | Italian | Portuguese (BR) | Chinese (Simplified) | Arabic |
|---------|---------|--------|---------|-----------------|----------------------|----------------|
| <img src="https://raw.githubusercontent.com/eibol/ffmpeg_batch/refs/heads/gh-pages/flags/us.png" width="16"> | <img src="https://raw.githubusercontent.com/eibol/ffmpeg_batch/refs/heads/gh-pages/flags/es.png" width="16"> | <img src="https://raw.githubusercontent.com/eibol/ffmpeg_batch/refs/heads/gh-pages/flags/fr.png" width="16"> | <img src="https://raw.githubusercontent.com/eibol/ffmpeg_batch/refs/heads/gh-pages/flags/it.png" width="16"> | <img src="https://raw.githubusercontent.com/eibol/ffmpeg_batch/refs/heads/gh-pages/flags/br.png" width="16"> |  <img src="https://raw.githubusercontent.com/eibol/ffmpeg_batch/refs/heads/gh-pages/flags/cn.png" width="16"> | <img src="https://raw.githubusercontent.com/eibol/ffmpeg_batch/refs/heads/gh-pages/flags/ar_eg.png" width="16">|

| Main tab   | Wizard   | Streams   | Subtitles  | URLs    |
|------------|----------|-----------|------------|---------|
| <img src="https://raw.githubusercontent.com/eibol/ffmpeg_batch/refs/heads/gh-pages/FFbatch_2024_main.jpg" width="250"> | <img src="https://raw.githubusercontent.com/eibol/ffmpeg_batch/refs/heads/gh-pages/FFbatch_2024_wizard.jpg" width="250"> | <img src="https://raw.githubusercontent.com/eibol/ffmpeg_batch/refs/heads/gh-pages/FFbatch_2024_mux.jpg" width="250"> | <img src="https://raw.githubusercontent.com/eibol/ffmpeg_batch/refs/heads/gh-pages/FFbatch_2024_subs.jpg" width="250"> | <img src="https://raw.githubusercontent.com/eibol/ffmpeg_batch/refs/heads/gh-pages/FFbatch_2024_urls.jpg" width="250"> |

To compile code, download all files to any folder.

NOTES:
- The application uses Aerowizard nuget package as mandatory.
- You can find these packages at nuget package manager inside Visual Studio.
- You need to create your own Code key for encryption, at menu Project -> Properties of FFBatch_Converter -> Signature, or disable manifest signing.
- You may have to run Visual Studio with admin rights at least once, after key generation.
