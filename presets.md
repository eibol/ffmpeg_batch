# FFmpeg Batch AV Converter collaborative presets

NOTE: Not all presets could be supported by FFmpeg Batch AV Converter, or only supported by using a wizard (those that reuse input files on parameters field require 
the use of variables like %1 or %fn). Please follow the table format below:

| Description         | Pre-Input        | Parameters                                         | Format |
| ------------------- | ---------------- | -------------------------------------------------- | ------ |
| Convert video to rgb uncompressed avi (huge file) |  | -c:v rawvideo -pix_fmt rgb24 | avi |
| Generate keyframes report to txt file | -skip_frame nokey | -vstats_file C:\Reports\stats.txt -vf "select='eq(pict_type,PICT_TYPE_I)'" -vsync vfr -frame_pts true -f null - | nul
