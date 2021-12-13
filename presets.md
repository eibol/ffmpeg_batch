# FFmpeg Batch AV Converter collaborative presets


NOTE: Not all presets could be supported by FFmpeg Batch AV Converter, or only supported by using a wizard (those that reuse input files on parameters field require 
the use of variables like %1 or %fn). Due to table formatting, if a preset fails, please try loading it raw from the application. Please follow the table format below:


| Description         | Pre-Input        | Parameters                             |Format|
| ------------------- | ---------------- | -------------------------------------- | ------ |
| Convert video to rgb uncompressed avi (huge file) |  | -c:v rawvideo -pix_fmt rgb24 | avi |
| Generate keyframes report to txt file | -skip_frame nokey | -vstats_file C:\Reports\stats.txt -vf "select='eq(pict_type,PICT_TYPE_I)'" -vsync vfr -frame_pts true -f null - | nul
| H265 one or two pass encoding usually 1 GB with good quality, with AAC audio | | -map 0 -c:v libx265 -preset medium -profile:v main10 -b:v 2000K  -x265-params "min-keyint=23;keyint=250;bframes=8;b-adapt=2;b-pyramid;bframe-bias=0;rc-lookahead=80;lookahead-slices=4;scenecut=40" -pix_fmt yuv420p10le -c:a aac -b:a 128K | mkv
|Convert vertical video with black sides, to video 16:9, with blurred background sides - 1080p | |-c:v libx264 -crf 23 -preset fast -lavfi "[0:v]scale=1920*2:1080*2,boxblur=luma_radius=min(h\,w)/20:luma_power=1:chroma_radius=min(cw\,ch)/20:chroma_power=1[bg];[0:v]scale=-1:1080[ov];[bg][ov]overlay=(W-w)/2:(H-h)/2,crop=w=1920:h=1080" -c:a copy |mp4 |
