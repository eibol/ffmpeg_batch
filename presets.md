# FFmpeg Batch AV Converter collaborative presets


NOTE: Not all presets could be supported by FFmpeg Batch AV Converter, or only supported by using a wizard (those that reuse input files on parameters field require 
the use of variables like %1 or %fn). Due to table formatting, if a preset fails, please try loading it raw from the application. Please follow the table format below:


| Description           | Pre-Input        | Parameters                           |Format|
| --------------------- | ---------------- | ------------------------------------ | ------ |
| Convert video to rgb uncompressed avi (huge file) |  | -c:v rawvideo -pix_fmt rgb24 | avi |
| Generate keyframes report to txt file | -skip_frame nokey | -vstats_file C:\Reports\stats.txt -vf "select='eq(pict_type,PICT_TYPE_I)'" -vsync vfr -frame_pts true -f null - | nul|
| H265 one or two pass encoding usually 1 GB with good quality, with AAC audio | | -map 0 -c:v libx265 -preset medium -profile:v main10 -b:v 2000K  -x265-params "min-keyint=23;keyint=250;bframes=8;b-adapt=2;b-pyramid;bframe-bias=0;rc-lookahead=80;lookahead-slices=4;scenecut=40" -pix_fmt yuv420p10le -c:a aac -b:a 128K | mkv |
| Convert vertical video with black sides, to video 16/9 with blurred background sides 720p | |-c:v libx264 -crf 21 -vf "split[original][copy];[copy]scale=ih*16/9:-1,crop=h=iw*9/16,gblur=sigma=20[blurred];[blurred][original]overlay=(main_w-overlay_w)/2:(main_h-overlay_h)/2,scale=-2:720" -c:a copy| mp4 |
| Convert vertical video with black sides, to video 16/9 with blurred background sides 1080p| |-c:v libx264 -crf 21 -vf "split[original][copy];[copy]scale=ih*16/9:-1,crop=h=iw*9/16,gblur=sigma=20[blurred];[blurred][original]overlay=(main_w-overlay_w)/2:(main_h-overlay_h)/2,scale=-2:1080" -c:a copy| mp4 |
| Reverse video and audio (recommended only for short clips due to memory usage)| |-c:v libx264 -crf 23 -vf reverse -af areverse -c:a aac -b:a 160K|mp4 |
| Create duplicated stereoscopic video output | |-c:v libx264 -crf 23 -vf stereo3d=al:sbsl -c:a copy |mp4|
| Create a 4 videos mosaic into one video. (You need first to add video1 to file list and match video names in parameters) | | -i C:\Videos\v2.mp4 -i C:\Videos\v3.mp4 -i C:\Videos\v4.mp4 -c:v libx264 -preset fast -crf 23 -filter_complex "[0:v][1:v][2:v][3:v]xstack=inputs=4:layout=0_0\|w0_0\|0_h0\|w0_h0[v]" -map "[v]" |mp4|
| Add top left overlay logo, display filename on top right corner, and video duration countdown at bottom right corner | | -i "C:\Datos\Test.png" -filter_complex "[1:v][0:v]scale2ref=(113/113)\*ih/13/sar:ih/13[wm][base];[base][wm]overlay=x=main_w\*0.01:y=main_h\*0.03,drawtext=fontsize=(h/40):fontcolor=white:fontfile=arial:text=%fn:x=w-(h/37)-300:y=(h/50),drawtext=fontfile=arial:text='\ %{eif\\:(mod((%fdur-t)/3600, 60))\\:d\\:2}\\:%{eif\\:(mod((%fdur-t)/60, 60))\\:d\\:2}\\:%{eif\\:(mod(%fdur-t, 60))\\:d\\:2}':fontcolor=white:fontsize=(h/40):x=w-tw-(h/37):y=h-th-(h/54):box=1:boxcolor=black@0.0:boxborderw=10" -c:v libx264 -crf 23 -c:a copy| mkv |
| Burn subtitles from source file  (first subtitle stream) encoded as h264 and audio stream copy to MKV | |-c:v libx264 -crf 23 -vf subtitles=%ff:stream_index=0 -c:a copy -sn | mkv |
| To use with trim button: Trim using streamcopy avoiding initial negative pts issue to MP4 | |-c copy -avoid_negative_ts make_zero | mp4 |
| Batch create videos from a single image and audio file to MP4 | -loop 1 -i "C:\yourfolder\YourImage.jpg" -r 10 |`-map 0:v:0 -c:v libx264 -preset veryfast -r 10 -crf 23 -tune stillimage -pix_fmt yuv420p -vf scale=1280:720 -map 1:a:0 -c:a aac -shortest` | mp4 |
