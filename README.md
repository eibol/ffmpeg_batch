# FFmpeg Batch AV Converter

FFmpeg Batch AV Converter is a ffmepg gui, a front-end for Windows ffmpeg users, and for Linux via Wine that allows the use of the full potential of ffmpeg command line with a few mouse clicks in a convenient GUI. Among other things, you can drag and drop, see progress information, change encoding priority, pause and 
resume, and set automatic shutdown. It is good for seasoned ffmpeg users as well as beginners.

Downloads: https://ffmpeg-batch.sourceforge.io

Support donations: https://bit.ly/3hAZrZU / https://www.paypal.me/eibolff 

It provides unlimited single or multi-file batch encoding for almost any audio/video format. You can use any set of parameters 
and try them before starting encoding. You can manipulate and multiplex streams, batch subtitle (as track and hardcoded), 
trim, concatenate, record screen, capture M3u8 or YouTube URLs. You can also access useful multimedia file information.

You can manually save your favourite custom ffmpeg parameters, using a fancy encoding wizard. You can use relative/absolute output 
paths, automatically rename output files, overwrite them etc.

FEATURES

   - Video encoding: AV1 / H264 / H265 / NVENC / QuickSync / ProRes / VP9 / Any other video format supported by ffmpeg.
   - Audio encoding: MP3 / AAC / AC3 / FLAC / WAV / Opus / Vorbis / Any other audio format supported by ffmpeg.
   - Multi-file encode for thousands of files
   - Automatic shutdown, with option to run post-encoding script
   - Batch processing
   - Set encoding priority
   - Drag and drop
   - Stream mapping and multiplex with jobs manager.
   - Batch download YouTube and m3u8 urls
   - FFmpeg parameters wizard
   - Filter files using different criteria.
   - Trim and concatenate files
   - Batch image thumbnail extraction

To compile code, please follow these steps:

- Download all files to any folder.
- In folder bin/debug, you have to place ffmpeg.exe, mediainfo.exe y yt-dlp.exe.

NOTES:
- The application uses Aerowizard nuget package as mandatory.
- You can find these packages at nuget package manager inside Visual Studio.
- You need to create your own Code key for encryption, at menu Project -> Properties of FFBatch_Converter -> Signature, or disable code signing.
- You may have to run Visual Studio 2019 with admin rights at least once, after key generation.
