
//  Microsoft (R) .NET Framework IL Disassembler.  Version 4.8.3928.0
//  (c) Microsoft Corporation. Reservados todos los derechos.



// Metadata version: v4.0.30319
.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 4:0:0:0
}
.assembly extern System
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 4:0:0:0
}
.assembly extern System.Xml.Linq
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 4:0:0:0
}
.assembly extern System.Xml
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 4:0:0:0
}
.assembly extern System.Configuration
{
  .publickeytoken = (B0 3F 5F 7F 11 D5 0A 3A )                         // .?_....:
  .ver 4:0:0:0
}
.assembly PortableSettingsProvider
{
  .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilationRelaxationsAttribute::.ctor(int32) = ( 01 00 08 00 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::.ctor() = ( 01 00 01 00 54 02 16 57 72 61 70 4E 6F 6E 45 78   // ....T..WrapNonEx
                                                                                                             63 65 70 74 69 6F 6E 54 68 72 6F 77 73 01 )       // ceptionThrows.

  // --- The following custom attribute is added automatically, do not uncomment -------
  //  .custom instance void [mscorlib]System.Diagnostics.DebuggableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggableAttribute/DebuggingModes) = ( 01 00 02 00 00 00 00 00 ) 

  .custom instance void [mscorlib]System.Runtime.Versioning.TargetFrameworkAttribute::.ctor(string) = ( 01 00 1A 2E 4E 45 54 46 72 61 6D 65 77 6F 72 6B   // ....NETFramework
                                                                                                        2C 56 65 72 73 69 6F 6E 3D 76 34 2E 35 01 00 54   // ,Version=v4.5..T
                                                                                                        0E 14 46 72 61 6D 65 77 6F 72 6B 44 69 73 70 6C   // ..FrameworkDispl
                                                                                                        61 79 4E 61 6D 65 12 2E 4E 45 54 20 46 72 61 6D   // ayName..NET Fram
                                                                                                        65 77 6F 72 6B 20 34 2E 35 )                      // ework 4.5
  .custom instance void [mscorlib]System.Reflection.AssemblyCompanyAttribute::.ctor(string) = ( 01 00 09 42 6C 75 65 67 72 61 6D 73 00 00 )       // ...Bluegrams..
  .custom instance void [mscorlib]System.Reflection.AssemblyConfigurationAttribute::.ctor(string) = ( 01 00 07 52 65 6C 65 61 73 65 00 00 )             // ...Release..
  .custom instance void [mscorlib]System.Reflection.AssemblyCopyrightAttribute::.ctor(string) = ( 01 00 16 C2 A9 20 32 30 31 38 2D 32 30 32 33 20   // ..... 2018-2023 
                                                                                                  42 6C 75 65 67 72 61 6D 73 00 00 )                // Bluegrams..
  .custom instance void [mscorlib]System.Reflection.AssemblyDescriptionAttribute::.ctor(string) = ( 01 00 80 BA 0D 0A 4D 61 6B 65 20 57 50 46 20 61   // ......Make WPF a
                                                                                                    6E 64 20 57 69 6E 46 6F 72 6D 73 20 73 65 74 74   // nd WinForms sett
                                                                                                    69 6E 67 73 20 70 6F 72 74 61 62 6C 65 20 62 79   // ings portable by
                                                                                                    20 73 61 76 69 6E 67 20 74 68 65 6D 20 69 6E 20   //  saving them in 
                                                                                                    74 68 65 20 61 70 70 6C 69 63 61 74 69 6F 6E 20   // the application 
                                                                                                    66 6F 6C 64 65 72 2E 0D 0A 52 65 61 64 20 6D 6F   // folder...Read mo
                                                                                                    72 65 20 61 74 20 68 74 74 70 73 3A 2F 2F 77 77   // re at https://ww
                                                                                                    77 2E 63 6F 64 65 70 72 6F 6A 65 63 74 2E 63 6F   // w.codeproject.co
                                                                                                    6D 2F 41 72 74 69 63 6C 65 73 2F 31 32 33 38 35   // m/Articles/12385
                                                                                                    35 30 2F 4D 61 6B 69 6E 67 2D 41 70 70 6C 69 63   // 50/Making-Applic
                                                                                                    61 74 69 6F 6E 2D 53 65 74 74 69 6E 67 73 2D 50   // ation-Settings-P
                                                                                                    6F 72 74 61 62 6C 65 2E 0D 0A 20 20 20 20 00 00 ) // ortable...    ..
  .custom instance void [mscorlib]System.Reflection.AssemblyFileVersionAttribute::.ctor(string) = ( 01 00 07 30 2E 32 2E 35 2E 30 00 00 )             // ...0.2.5.0..
  .custom instance void [mscorlib]System.Reflection.AssemblyInformationalVersionAttribute::.ctor(string) = ( 01 00 05 30 2E 32 2E 35 00 00 )                   // ...0.2.5..
  .custom instance void [mscorlib]System.Reflection.AssemblyProductAttribute::.ctor(string) = ( 01 00 18 50 6F 72 74 61 62 6C 65 53 65 74 74 69   // ...PortableSetti
                                                                                                6E 67 73 50 72 6F 76 69 64 65 72 00 00 )          // ngsProvider..
  .custom instance void [mscorlib]System.Reflection.AssemblyTitleAttribute::.ctor(string) = ( 01 00 18 50 6F 72 74 61 62 6C 65 53 65 74 74 69   // ...PortableSetti
                                                                                              6E 67 73 50 72 6F 76 69 64 65 72 00 00 )          // ngsProvider..
  .hash algorithm 0x00008004
  .ver 0:2:5:0
}
.module PortableSettingsProvider.dll
// MVID: {E10BEE2D-C281-4BDE-B0F0-BEF057A0F594}
.imagebase 0x10000000
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0003       // WINDOWS_CUI
.corflags 0x00000001    //  ILONLY
// Image base: 0x06980000


// =============== CLASS MEMBERS DECLARATION ===================

.class public auto ansi beforefieldinit Bluegrams.Application.PortableSettingsProvider
       extends Bluegrams.Application.PortableSettingsProviderBase
{
  .field private static string '<SettingsFileName>k__BackingField'
  .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
  .method public hidebysig specialname static 
          string  get_SettingsFileName() cil managed
  {
    .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    // Code size       6 (0x6)
    .maxstack  8
    IL_0000:  ldsfld     string Bluegrams.Application.PortableSettingsProvider::'<SettingsFileName>k__BackingField'
    IL_0005:  ret
  } // end of method PortableSettingsProvider::get_SettingsFileName

  .method public hidebysig specialname static 
          void  set_SettingsFileName(string 'value') cil managed
  {
    .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    // Code size       7 (0x7)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  stsfld     string Bluegrams.Application.PortableSettingsProvider::'<SettingsFileName>k__BackingField'
    IL_0006:  ret
  } // end of method PortableSettingsProvider::set_SettingsFileName

  .method public hidebysig specialname virtual 
          instance string  get_Name() cil managed
  {
    // Code size       6 (0x6)
    .maxstack  8
    IL_0000:  ldstr      "PortableSettingsProvider"
    IL_0005:  ret
  } // end of method PortableSettingsProvider::get_Name

  .method public hidebysig static void  ApplyProvider(class [System]System.Configuration.ApplicationSettingsBase[] settingsList) cil managed
  {
    .param [1]
    .custom instance void [mscorlib]System.ParamArrayAttribute::.ctor() = ( 01 00 00 00 ) 
    // Code size       12 (0xc)
    .maxstack  8
    IL_0000:  newobj     instance void Bluegrams.Application.PortableSettingsProvider::.ctor()
    IL_0005:  ldarg.0
    IL_0006:  call       void Bluegrams.Application.PortableSettingsProviderBase::ApplyProvider(class Bluegrams.Application.PortableSettingsProviderBase,
                                                                                                class [System]System.Configuration.ApplicationSettingsBase[])
    IL_000b:  ret
  } // end of method PortableSettingsProvider::ApplyProvider

  .method private hidebysig specialname instance string 
          get_ApplicationSettingsFile() cil managed
  {
    // Code size       16 (0x10)
    .maxstack  8
    IL_0000:  call       string Bluegrams.Application.PortableSettingsProviderBase::get_SettingsDirectory()
    IL_0005:  call       string Bluegrams.Application.PortableSettingsProvider::get_SettingsFileName()
    IL_000a:  call       string [mscorlib]System.IO.Path::Combine(string,
                                                                  string)
    IL_000f:  ret
  } // end of method PortableSettingsProvider::get_ApplicationSettingsFile

  .method public hidebysig virtual instance void 
          Reset(class [System]System.Configuration.SettingsContext context) cil managed
  {
    // Code size       25 (0x19)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance string Bluegrams.Application.PortableSettingsProvider::get_ApplicationSettingsFile()
    IL_0006:  call       bool [mscorlib]System.IO.File::Exists(string)
    IL_000b:  brfalse.s  IL_0018

    IL_000d:  ldarg.0
    IL_000e:  call       instance string Bluegrams.Application.PortableSettingsProvider::get_ApplicationSettingsFile()
    IL_0013:  call       void [mscorlib]System.IO.File::Delete(string)
    IL_0018:  ret
  } // end of method PortableSettingsProvider::Reset

  .method private hidebysig instance class [System.Xml.Linq]System.Xml.Linq.XDocument 
          GetXmlDoc() cil managed
  {
    // Code size       103 (0x67)
    .maxstack  6
    .locals init (class [System.Xml.Linq]System.Xml.Linq.XDocument V_0,
             bool V_1)
    IL_0000:  ldnull
    IL_0001:  stloc.0
    IL_0002:  ldc.i4.0
    IL_0003:  stloc.1
    IL_0004:  ldarg.0
    IL_0005:  call       instance string Bluegrams.Application.PortableSettingsProvider::get_ApplicationSettingsFile()
    IL_000a:  call       bool [mscorlib]System.IO.File::Exists(string)
    IL_000f:  brfalse.s  IL_0024

    .try
    {
      IL_0011:  ldarg.0
      IL_0012:  call       instance string Bluegrams.Application.PortableSettingsProvider::get_ApplicationSettingsFile()
      IL_0017:  call       class [System.Xml.Linq]System.Xml.Linq.XDocument [System.Xml.Linq]System.Xml.Linq.XDocument::Load(string)
      IL_001c:  stloc.0
      IL_001d:  leave.s    IL_0026

    }  // end .try
    catch [mscorlib]System.Object 
    {
      IL_001f:  pop
      IL_0020:  ldc.i4.1
      IL_0021:  stloc.1
      IL_0022:  leave.s    IL_0026

    }  // end handler
    IL_0024:  ldc.i4.1
    IL_0025:  stloc.1
    IL_0026:  ldloc.1
    IL_0027:  brfalse.s  IL_0065

    IL_0029:  ldc.i4.1
    IL_002a:  newarr     [mscorlib]System.Object
    IL_002f:  dup
    IL_0030:  ldc.i4.0
    IL_0031:  ldstr      "configuration"
    IL_0036:  call       class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::op_Implicit(string)
    IL_003b:  ldstr      "userSettings"
    IL_0040:  call       class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::op_Implicit(string)
    IL_0045:  ldstr      "Roaming"
    IL_004a:  call       class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::op_Implicit(string)
    IL_004f:  newobj     instance void [System.Xml.Linq]System.Xml.Linq.XElement::.ctor(class [System.Xml.Linq]System.Xml.Linq.XName)
    IL_0054:  newobj     instance void [System.Xml.Linq]System.Xml.Linq.XElement::.ctor(class [System.Xml.Linq]System.Xml.Linq.XName,
                                                                                        object)
    IL_0059:  newobj     instance void [System.Xml.Linq]System.Xml.Linq.XElement::.ctor(class [System.Xml.Linq]System.Xml.Linq.XName,
                                                                                        object)
    IL_005e:  stelem.ref
    IL_005f:  newobj     instance void [System.Xml.Linq]System.Xml.Linq.XDocument::.ctor(object[])
    IL_0064:  stloc.0
    IL_0065:  ldloc.0
    IL_0066:  ret
  } // end of method PortableSettingsProvider::GetXmlDoc

  .method public hidebysig virtual instance class [System]System.Configuration.SettingsPropertyValueCollection 
          GetPropertyValues(class [System]System.Configuration.SettingsContext context,
                            class [System]System.Configuration.SettingsPropertyCollection collection) cil managed
  {
    // Code size       126 (0x7e)
    .maxstack  5
    .locals init (class [System.Xml.Linq]System.Xml.Linq.XDocument V_0,
             class [System]System.Configuration.SettingsPropertyValueCollection V_1,
             class [mscorlib]System.Collections.IEnumerator V_2,
             class [System]System.Configuration.SettingsProperty V_3,
             class [System]System.Configuration.SettingsPropertyValue V_4,
             class [mscorlib]System.IDisposable V_5)
    IL_0000:  ldarg.0
    IL_0001:  call       instance class [System.Xml.Linq]System.Xml.Linq.XDocument Bluegrams.Application.PortableSettingsProvider::GetXmlDoc()
    IL_0006:  stloc.0
    IL_0007:  newobj     instance void [System]System.Configuration.SettingsPropertyValueCollection::.ctor()
    IL_000c:  stloc.1
    IL_000d:  ldarg.2
    IL_000e:  callvirt   instance class [mscorlib]System.Collections.IEnumerator [System]System.Configuration.SettingsPropertyCollection::GetEnumerator()
    IL_0013:  stloc.2
    .try
    {
      IL_0014:  br.s       IL_005e

      IL_0016:  ldloc.2
      IL_0017:  callvirt   instance object [mscorlib]System.Collections.IEnumerator::get_Current()
      IL_001c:  castclass  [System]System.Configuration.SettingsProperty
      IL_0021:  stloc.3
      IL_0022:  ldloc.3
      IL_0023:  newobj     instance void [System]System.Configuration.SettingsPropertyValue::.ctor(class [System]System.Configuration.SettingsProperty)
      IL_0028:  stloc.s    V_4
      IL_002a:  ldloc.s    V_4
      IL_002c:  ldc.i4.0
      IL_002d:  callvirt   instance void [System]System.Configuration.SettingsPropertyValue::set_IsDirty(bool)
      IL_0032:  ldloc.s    V_4
      IL_0034:  ldarg.0
      IL_0035:  ldloc.0
      IL_0036:  ldarg.1
      IL_0037:  ldstr      "GroupName"
      IL_003c:  callvirt   instance object [mscorlib]System.Collections.Hashtable::get_Item(object)
      IL_0041:  castclass  [mscorlib]System.String
      IL_0046:  call       string [System.Xml]System.Xml.XmlConvert::EncodeLocalName(string)
      IL_004b:  ldloc.3
      IL_004c:  call       instance object Bluegrams.Application.PortableSettingsProvider::getXmlValue(class [System.Xml.Linq]System.Xml.Linq.XDocument,
                                                                                                       string,
                                                                                                       class [System]System.Configuration.SettingsProperty)
      IL_0051:  callvirt   instance void [System]System.Configuration.SettingsPropertyValue::set_SerializedValue(object)
      IL_0056:  ldloc.1
      IL_0057:  ldloc.s    V_4
      IL_0059:  callvirt   instance void [System]System.Configuration.SettingsPropertyValueCollection::Add(class [System]System.Configuration.SettingsPropertyValue)
      IL_005e:  ldloc.2
      IL_005f:  callvirt   instance bool [mscorlib]System.Collections.IEnumerator::MoveNext()
      IL_0064:  brtrue.s   IL_0016

      IL_0066:  leave.s    IL_007c

    }  // end .try
    finally
    {
      IL_0068:  ldloc.2
      IL_0069:  isinst     [mscorlib]System.IDisposable
      IL_006e:  stloc.s    V_5
      IL_0070:  ldloc.s    V_5
      IL_0072:  brfalse.s  IL_007b

      IL_0074:  ldloc.s    V_5
      IL_0076:  callvirt   instance void [mscorlib]System.IDisposable::Dispose()
      IL_007b:  endfinally
    }  // end handler
    IL_007c:  ldloc.1
    IL_007d:  ret
  } // end of method PortableSettingsProvider::GetPropertyValues

  .method public hidebysig virtual instance void 
          SetPropertyValues(class [System]System.Configuration.SettingsContext context,
                            class [System]System.Configuration.SettingsPropertyValueCollection collection) cil managed
  {
    // Code size       145 (0x91)
    .maxstack  4
    .locals init (class [System.Xml.Linq]System.Xml.Linq.XDocument V_0,
             class [mscorlib]System.Collections.IEnumerator V_1,
             class [System]System.Configuration.SettingsPropertyValue V_2,
             class [mscorlib]System.IDisposable V_3,
             class [System.Xml]System.Xml.XmlWriter V_4)
    IL_0000:  ldarg.0
    IL_0001:  call       instance class [System.Xml.Linq]System.Xml.Linq.XDocument Bluegrams.Application.PortableSettingsProvider::GetXmlDoc()
    IL_0006:  stloc.0
    IL_0007:  ldarg.2
    IL_0008:  callvirt   instance class [mscorlib]System.Collections.IEnumerator [System]System.Configuration.SettingsPropertyValueCollection::GetEnumerator()
    IL_000d:  stloc.1
    .try
    {
      IL_000e:  br.s       IL_0039

      IL_0010:  ldloc.1
      IL_0011:  callvirt   instance object [mscorlib]System.Collections.IEnumerator::get_Current()
      IL_0016:  castclass  [System]System.Configuration.SettingsPropertyValue
      IL_001b:  stloc.2
      IL_001c:  ldarg.0
      IL_001d:  ldloc.0
      IL_001e:  ldarg.1
      IL_001f:  ldstr      "GroupName"
      IL_0024:  callvirt   instance object [mscorlib]System.Collections.Hashtable::get_Item(object)
      IL_0029:  castclass  [mscorlib]System.String
      IL_002e:  call       string [System.Xml]System.Xml.XmlConvert::EncodeLocalName(string)
      IL_0033:  ldloc.2
      IL_0034:  call       instance void Bluegrams.Application.PortableSettingsProvider::setXmlValue(class [System.Xml.Linq]System.Xml.Linq.XDocument,
                                                                                                     string,
                                                                                                     class [System]System.Configuration.SettingsPropertyValue)
      IL_0039:  ldloc.1
      IL_003a:  callvirt   instance bool [mscorlib]System.Collections.IEnumerator::MoveNext()
      IL_003f:  brtrue.s   IL_0010

      IL_0041:  leave.s    IL_0054

    }  // end .try
    finally
    {
      IL_0043:  ldloc.1
      IL_0044:  isinst     [mscorlib]System.IDisposable
      IL_0049:  stloc.3
      IL_004a:  ldloc.3
      IL_004b:  brfalse.s  IL_0053

      IL_004d:  ldloc.3
      IL_004e:  callvirt   instance void [mscorlib]System.IDisposable::Dispose()
      IL_0053:  endfinally
    }  // end handler
    IL_0054:  nop
    .try
    {
      IL_0055:  ldarg.0
      IL_0056:  call       instance string Bluegrams.Application.PortableSettingsProvider::get_ApplicationSettingsFile()
      IL_005b:  newobj     instance void [System.Xml]System.Xml.XmlWriterSettings::.ctor()
      IL_0060:  dup
      IL_0061:  ldc.i4.1
      IL_0062:  callvirt   instance void [System.Xml]System.Xml.XmlWriterSettings::set_NewLineHandling(valuetype [System.Xml]System.Xml.NewLineHandling)
      IL_0067:  dup
      IL_0068:  ldc.i4.1
      IL_0069:  callvirt   instance void [System.Xml]System.Xml.XmlWriterSettings::set_Indent(bool)
      IL_006e:  call       class [System.Xml]System.Xml.XmlWriter [System.Xml]System.Xml.XmlWriter::Create(string,
                                                                                                           class [System.Xml]System.Xml.XmlWriterSettings)
      IL_0073:  stloc.s    V_4
      .try
      {
        IL_0075:  ldloc.0
        IL_0076:  ldloc.s    V_4
        IL_0078:  callvirt   instance void [System.Xml.Linq]System.Xml.Linq.XDocument::Save(class [System.Xml]System.Xml.XmlWriter)
        IL_007d:  leave.s    IL_008b

      }  // end .try
      finally
      {
        IL_007f:  ldloc.s    V_4
        IL_0081:  brfalse.s  IL_008a

        IL_0083:  ldloc.s    V_4
        IL_0085:  callvirt   instance void [mscorlib]System.IDisposable::Dispose()
        IL_008a:  endfinally
      }  // end handler
      IL_008b:  leave.s    IL_0090

    }  // end .try
    catch [mscorlib]System.Object 
    {
      IL_008d:  pop
      IL_008e:  leave.s    IL_0090

    }  // end handler
    IL_0090:  ret
  } // end of method PortableSettingsProvider::SetPropertyValues

  .method private hidebysig instance object 
          getXmlValue(class [System.Xml.Linq]System.Xml.Linq.XDocument xmlDoc,
                      string scope,
                      class [System]System.Configuration.SettingsProperty prop) cil managed
  {
    // Code size       269 (0x10d)
    .maxstack  3
    .locals init (object V_0,
             class [System.Xml.Linq]System.Xml.Linq.XElement V_1,
             class [System.Xml]System.Xml.XmlReader V_2,
             valuetype [System]System.Configuration.SettingsSerializeAs V_3)
    IL_0000:  ldnull
    IL_0001:  stloc.0
    IL_0002:  ldarg.0
    IL_0003:  ldarg.3
    IL_0004:  call       instance bool Bluegrams.Application.PortableSettingsProviderBase::IsUserScoped(class [System]System.Configuration.SettingsProperty)
    IL_0009:  brtrue.s   IL_000d

    IL_000b:  ldloc.0
    IL_000c:  ret

    IL_000d:  ldarg.1
    IL_000e:  ldstr      "configuration"
    IL_0013:  call       class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::op_Implicit(string)
    IL_0018:  callvirt   instance class [System.Xml.Linq]System.Xml.Linq.XElement [System.Xml.Linq]System.Xml.Linq.XContainer::Element(class [System.Xml.Linq]System.Xml.Linq.XName)
    IL_001d:  ldstr      "userSettings"
    IL_0022:  call       class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::op_Implicit(string)
    IL_0027:  callvirt   instance class [System.Xml.Linq]System.Xml.Linq.XElement [System.Xml.Linq]System.Xml.Linq.XContainer::Element(class [System.Xml.Linq]System.Xml.Linq.XName)
    IL_002c:  stloc.1
    IL_002d:  ldarg.0
    IL_002e:  ldarg.3
    IL_002f:  call       instance bool Bluegrams.Application.PortableSettingsProviderBase::IsRoaming(class [System]System.Configuration.SettingsProperty)
    IL_0034:  brfalse.s  IL_0049

    IL_0036:  ldloc.1
    IL_0037:  ldstr      "Roaming"
    IL_003c:  call       class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::op_Implicit(string)
    IL_0041:  callvirt   instance class [System.Xml.Linq]System.Xml.Linq.XElement [System.Xml.Linq]System.Xml.Linq.XContainer::Element(class [System.Xml.Linq]System.Xml.Linq.XName)
    IL_0046:  stloc.1
    IL_0047:  br.s       IL_0064

    IL_0049:  ldloc.1
    IL_004a:  ldstr      "PC_"
    IL_004f:  call       string [mscorlib]System.Environment::get_MachineName()
    IL_0054:  call       string [mscorlib]System.String::Concat(string,
                                                                string)
    IL_0059:  call       class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::op_Implicit(string)
    IL_005e:  callvirt   instance class [System.Xml.Linq]System.Xml.Linq.XElement [System.Xml.Linq]System.Xml.Linq.XContainer::Element(class [System.Xml.Linq]System.Xml.Linq.XName)
    IL_0063:  stloc.1
    IL_0064:  ldloc.1
    IL_0065:  brfalse    IL_0104

    IL_006a:  ldloc.1
    IL_006b:  ldarg.2
    IL_006c:  call       class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::op_Implicit(string)
    IL_0071:  callvirt   instance class [System.Xml.Linq]System.Xml.Linq.XElement [System.Xml.Linq]System.Xml.Linq.XContainer::Element(class [System.Xml.Linq]System.Xml.Linq.XName)
    IL_0076:  brfalse    IL_0104

    IL_007b:  ldloc.1
    IL_007c:  ldarg.2
    IL_007d:  call       class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::op_Implicit(string)
    IL_0082:  callvirt   instance class [System.Xml.Linq]System.Xml.Linq.XElement [System.Xml.Linq]System.Xml.Linq.XContainer::Element(class [System.Xml.Linq]System.Xml.Linq.XName)
    IL_0087:  ldarg.3
    IL_0088:  callvirt   instance string [System]System.Configuration.SettingsProperty::get_Name()
    IL_008d:  call       class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::op_Implicit(string)
    IL_0092:  callvirt   instance class [System.Xml.Linq]System.Xml.Linq.XElement [System.Xml.Linq]System.Xml.Linq.XContainer::Element(class [System.Xml.Linq]System.Xml.Linq.XName)
    IL_0097:  brfalse.s  IL_0104

    IL_0099:  ldloc.1
    IL_009a:  ldarg.2
    IL_009b:  call       class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::op_Implicit(string)
    IL_00a0:  callvirt   instance class [System.Xml.Linq]System.Xml.Linq.XElement [System.Xml.Linq]System.Xml.Linq.XContainer::Element(class [System.Xml.Linq]System.Xml.Linq.XName)
    IL_00a5:  ldarg.3
    IL_00a6:  callvirt   instance string [System]System.Configuration.SettingsProperty::get_Name()
    IL_00ab:  call       class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::op_Implicit(string)
    IL_00b0:  callvirt   instance class [System.Xml.Linq]System.Xml.Linq.XElement [System.Xml.Linq]System.Xml.Linq.XContainer::Element(class [System.Xml.Linq]System.Xml.Linq.XName)
    IL_00b5:  callvirt   instance class [System.Xml]System.Xml.XmlReader [System.Xml.Linq]System.Xml.Linq.XNode::CreateReader()
    IL_00ba:  stloc.2
    .try
    {
      IL_00bb:  ldloc.2
      IL_00bc:  callvirt   instance valuetype [System.Xml]System.Xml.XmlNodeType [System.Xml]System.Xml.XmlReader::MoveToContent()
      IL_00c1:  pop
      IL_00c2:  ldarg.3
      IL_00c3:  callvirt   instance valuetype [System]System.Configuration.SettingsSerializeAs [System]System.Configuration.SettingsProperty::get_SerializeAs()
      IL_00c8:  stloc.3
      IL_00c9:  ldloc.3
      IL_00ca:  ldc.i4.1
      IL_00cb:  beq.s      IL_00d3

      IL_00cd:  ldloc.3
      IL_00ce:  ldc.i4.2
      IL_00cf:  beq.s      IL_00dc

      IL_00d1:  br.s       IL_00f1

      IL_00d3:  ldloc.2
      IL_00d4:  callvirt   instance string [System.Xml]System.Xml.XmlReader::ReadInnerXml()
      IL_00d9:  stloc.0
      IL_00da:  leave.s    IL_010b

      IL_00dc:  ldloc.2
      IL_00dd:  callvirt   instance string [System.Xml]System.Xml.XmlReader::ReadInnerXml()
      IL_00e2:  stloc.0
      IL_00e3:  ldloc.0
      IL_00e4:  isinst     [mscorlib]System.String
      IL_00e9:  call       uint8[] [mscorlib]System.Convert::FromBase64String(string)
      IL_00ee:  stloc.0
      IL_00ef:  leave.s    IL_010b

      IL_00f1:  ldloc.2
      IL_00f2:  callvirt   instance string [System.Xml]System.Xml.XmlReader::ReadElementContentAsString()
      IL_00f7:  stloc.0
      IL_00f8:  leave.s    IL_010b

    }  // end .try
    finally
    {
      IL_00fa:  ldloc.2
      IL_00fb:  brfalse.s  IL_0103

      IL_00fd:  ldloc.2
      IL_00fe:  callvirt   instance void [mscorlib]System.IDisposable::Dispose()
      IL_0103:  endfinally
    }  // end handler
    IL_0104:  ldarg.3
    IL_0105:  callvirt   instance object [System]System.Configuration.SettingsProperty::get_DefaultValue()
    IL_010a:  stloc.0
    IL_010b:  ldloc.0
    IL_010c:  ret
  } // end of method PortableSettingsProvider::getXmlValue

  .method private hidebysig instance void 
          setXmlValue(class [System.Xml.Linq]System.Xml.Linq.XDocument xmlDoc,
                      string scope,
                      class [System]System.Configuration.SettingsPropertyValue 'value') cil managed
  {
    // Code size       453 (0x1c5)
    .maxstack  4
    .locals init (class [System.Xml.Linq]System.Xml.Linq.XElement V_0,
             class [System.Xml.Linq]System.Xml.Linq.XElement V_1,
             class [System.Xml.Linq]System.Xml.Linq.XNode V_2,
             string V_3,
             class [System.Xml.Linq]System.Xml.Linq.XElement V_4,
             class [System.Xml.Linq]System.Xml.Linq.XElement V_5)
    IL_0000:  ldarg.0
    IL_0001:  ldarg.3
    IL_0002:  callvirt   instance class [System]System.Configuration.SettingsProperty [System]System.Configuration.SettingsPropertyValue::get_Property()
    IL_0007:  call       instance bool Bluegrams.Application.PortableSettingsProviderBase::IsUserScoped(class [System]System.Configuration.SettingsProperty)
    IL_000c:  brtrue.s   IL_000f

    IL_000e:  ret

    IL_000f:  ldarg.1
    IL_0010:  ldstr      "configuration"
    IL_0015:  call       class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::op_Implicit(string)
    IL_001a:  callvirt   instance class [System.Xml.Linq]System.Xml.Linq.XElement [System.Xml.Linq]System.Xml.Linq.XContainer::Element(class [System.Xml.Linq]System.Xml.Linq.XName)
    IL_001f:  ldstr      "userSettings"
    IL_0024:  call       class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::op_Implicit(string)
    IL_0029:  callvirt   instance class [System.Xml.Linq]System.Xml.Linq.XElement [System.Xml.Linq]System.Xml.Linq.XContainer::Element(class [System.Xml.Linq]System.Xml.Linq.XName)
    IL_002e:  stloc.0
    IL_002f:  ldarg.0
    IL_0030:  ldarg.3
    IL_0031:  callvirt   instance class [System]System.Configuration.SettingsProperty [System]System.Configuration.SettingsPropertyValue::get_Property()
    IL_0036:  call       instance bool Bluegrams.Application.PortableSettingsProviderBase::IsRoaming(class [System]System.Configuration.SettingsProperty)
    IL_003b:  brfalse.s  IL_0050

    IL_003d:  ldloc.0
    IL_003e:  ldstr      "Roaming"
    IL_0043:  call       class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::op_Implicit(string)
    IL_0048:  callvirt   instance class [System.Xml.Linq]System.Xml.Linq.XElement [System.Xml.Linq]System.Xml.Linq.XContainer::Element(class [System.Xml.Linq]System.Xml.Linq.XName)
    IL_004d:  stloc.1
    IL_004e:  br.s       IL_006b

    IL_0050:  ldloc.0
    IL_0051:  ldstr      "PC_"
    IL_0056:  call       string [mscorlib]System.Environment::get_MachineName()
    IL_005b:  call       string [mscorlib]System.String::Concat(string,
                                                                string)
    IL_0060:  call       class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::op_Implicit(string)
    IL_0065:  callvirt   instance class [System.Xml.Linq]System.Xml.Linq.XElement [System.Xml.Linq]System.Xml.Linq.XContainer::Element(class [System.Xml.Linq]System.Xml.Linq.XName)
    IL_006a:  stloc.1
    IL_006b:  ldarg.3
    IL_006c:  callvirt   instance object [System]System.Configuration.SettingsPropertyValue::get_SerializedValue()
    IL_0071:  brfalse.s  IL_008a

    IL_0073:  ldarg.3
    IL_0074:  callvirt   instance object [System]System.Configuration.SettingsPropertyValue::get_SerializedValue()
    IL_0079:  isinst     [mscorlib]System.String
    IL_007e:  stloc.3
    IL_007f:  ldloc.3
    IL_0080:  brfalse.s  IL_0097

    IL_0082:  ldloc.3
    IL_0083:  call       bool [mscorlib]System.String::IsNullOrWhiteSpace(string)
    IL_0088:  brfalse.s  IL_0097

    IL_008a:  ldstr      ""
    IL_008f:  newobj     instance void [System.Xml.Linq]System.Xml.Linq.XText::.ctor(string)
    IL_0094:  stloc.2
    IL_0095:  br.s       IL_00ef

    IL_0097:  ldarg.3
    IL_0098:  callvirt   instance class [System]System.Configuration.SettingsProperty [System]System.Configuration.SettingsPropertyValue::get_Property()
    IL_009d:  callvirt   instance valuetype [System]System.Configuration.SettingsSerializeAs [System]System.Configuration.SettingsProperty::get_SerializeAs()
    IL_00a2:  ldc.i4.1
    IL_00a3:  bne.un.s   IL_00b8

    IL_00a5:  ldarg.3
    IL_00a6:  callvirt   instance object [System]System.Configuration.SettingsPropertyValue::get_SerializedValue()
    IL_00ab:  castclass  [mscorlib]System.String
    IL_00b0:  call       class [System.Xml.Linq]System.Xml.Linq.XElement [System.Xml.Linq]System.Xml.Linq.XElement::Parse(string)
    IL_00b5:  stloc.2
    IL_00b6:  br.s       IL_00ef

    IL_00b8:  ldarg.3
    IL_00b9:  callvirt   instance class [System]System.Configuration.SettingsProperty [System]System.Configuration.SettingsPropertyValue::get_Property()
    IL_00be:  callvirt   instance valuetype [System]System.Configuration.SettingsSerializeAs [System]System.Configuration.SettingsProperty::get_SerializeAs()
    IL_00c3:  ldc.i4.2
    IL_00c4:  bne.un.s   IL_00de

    IL_00c6:  ldarg.3
    IL_00c7:  callvirt   instance object [System]System.Configuration.SettingsPropertyValue::get_SerializedValue()
    IL_00cc:  castclass  uint8[]
    IL_00d1:  call       string [mscorlib]System.Convert::ToBase64String(uint8[])
    IL_00d6:  newobj     instance void [System.Xml.Linq]System.Xml.Linq.XText::.ctor(string)
    IL_00db:  stloc.2
    IL_00dc:  br.s       IL_00ef

    IL_00de:  ldarg.3
    IL_00df:  callvirt   instance object [System]System.Configuration.SettingsPropertyValue::get_SerializedValue()
    IL_00e4:  castclass  [mscorlib]System.String
    IL_00e9:  newobj     instance void [System.Xml.Linq]System.Xml.Linq.XText::.ctor(string)
    IL_00ee:  stloc.2
    IL_00ef:  ldloc.1
    IL_00f0:  brtrue.s   IL_0156

    IL_00f2:  ldarg.0
    IL_00f3:  ldarg.3
    IL_00f4:  callvirt   instance class [System]System.Configuration.SettingsProperty [System]System.Configuration.SettingsPropertyValue::get_Property()
    IL_00f9:  call       instance bool Bluegrams.Application.PortableSettingsProviderBase::IsRoaming(class [System]System.Configuration.SettingsProperty)
    IL_00fe:  brfalse.s  IL_0112

    IL_0100:  ldstr      "Roaming"
    IL_0105:  call       class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::op_Implicit(string)
    IL_010a:  newobj     instance void [System.Xml.Linq]System.Xml.Linq.XElement::.ctor(class [System.Xml.Linq]System.Xml.Linq.XName)
    IL_010f:  stloc.1
    IL_0110:  br.s       IL_012c

    IL_0112:  ldstr      "PC_"
    IL_0117:  call       string [mscorlib]System.Environment::get_MachineName()
    IL_011c:  call       string [mscorlib]System.String::Concat(string,
                                                                string)
    IL_0121:  call       class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::op_Implicit(string)
    IL_0126:  newobj     instance void [System.Xml.Linq]System.Xml.Linq.XElement::.ctor(class [System.Xml.Linq]System.Xml.Linq.XName)
    IL_012b:  stloc.1
    IL_012c:  ldloc.1
    IL_012d:  ldarg.2
    IL_012e:  call       class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::op_Implicit(string)
    IL_0133:  ldarg.3
    IL_0134:  callvirt   instance string [System]System.Configuration.SettingsPropertyValue::get_Name()
    IL_0139:  call       class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::op_Implicit(string)
    IL_013e:  ldloc.2
    IL_013f:  newobj     instance void [System.Xml.Linq]System.Xml.Linq.XElement::.ctor(class [System.Xml.Linq]System.Xml.Linq.XName,
                                                                                        object)
    IL_0144:  newobj     instance void [System.Xml.Linq]System.Xml.Linq.XElement::.ctor(class [System.Xml.Linq]System.Xml.Linq.XName,
                                                                                        object)
    IL_0149:  callvirt   instance void [System.Xml.Linq]System.Xml.Linq.XContainer::Add(object)
    IL_014e:  ldloc.0
    IL_014f:  ldloc.1
    IL_0150:  callvirt   instance void [System.Xml.Linq]System.Xml.Linq.XContainer::Add(object)
    IL_0155:  ret

    IL_0156:  ldloc.1
    IL_0157:  ldarg.2
    IL_0158:  call       class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::op_Implicit(string)
    IL_015d:  callvirt   instance class [System.Xml.Linq]System.Xml.Linq.XElement [System.Xml.Linq]System.Xml.Linq.XContainer::Element(class [System.Xml.Linq]System.Xml.Linq.XName)
    IL_0162:  stloc.s    V_4
    IL_0164:  ldloc.s    V_4
    IL_0166:  brfalse.s  IL_01a2

    IL_0168:  ldloc.s    V_4
    IL_016a:  ldarg.3
    IL_016b:  callvirt   instance string [System]System.Configuration.SettingsPropertyValue::get_Name()
    IL_0170:  call       class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::op_Implicit(string)
    IL_0175:  callvirt   instance class [System.Xml.Linq]System.Xml.Linq.XElement [System.Xml.Linq]System.Xml.Linq.XContainer::Element(class [System.Xml.Linq]System.Xml.Linq.XName)
    IL_017a:  stloc.s    V_5
    IL_017c:  ldloc.s    V_5
    IL_017e:  brtrue.s   IL_0199

    IL_0180:  ldloc.s    V_4
    IL_0182:  ldarg.3
    IL_0183:  callvirt   instance string [System]System.Configuration.SettingsPropertyValue::get_Name()
    IL_0188:  call       class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::op_Implicit(string)
    IL_018d:  ldloc.2
    IL_018e:  newobj     instance void [System.Xml.Linq]System.Xml.Linq.XElement::.ctor(class [System.Xml.Linq]System.Xml.Linq.XName,
                                                                                        object)
    IL_0193:  callvirt   instance void [System.Xml.Linq]System.Xml.Linq.XContainer::Add(object)
    IL_0198:  ret

    IL_0199:  ldloc.s    V_5
    IL_019b:  ldloc.2
    IL_019c:  callvirt   instance void [System.Xml.Linq]System.Xml.Linq.XElement::ReplaceAll(object)
    IL_01a1:  ret

    IL_01a2:  ldloc.1
    IL_01a3:  ldarg.2
    IL_01a4:  call       class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::op_Implicit(string)
    IL_01a9:  ldarg.3
    IL_01aa:  callvirt   instance string [System]System.Configuration.SettingsPropertyValue::get_Name()
    IL_01af:  call       class [System.Xml.Linq]System.Xml.Linq.XName [System.Xml.Linq]System.Xml.Linq.XName::op_Implicit(string)
    IL_01b4:  ldloc.2
    IL_01b5:  newobj     instance void [System.Xml.Linq]System.Xml.Linq.XElement::.ctor(class [System.Xml.Linq]System.Xml.Linq.XName,
                                                                                        object)
    IL_01ba:  newobj     instance void [System.Xml.Linq]System.Xml.Linq.XElement::.ctor(class [System.Xml.Linq]System.Xml.Linq.XName,
                                                                                        object)
    IL_01bf:  callvirt   instance void [System.Xml.Linq]System.Xml.Linq.XContainer::Add(object)
    IL_01c4:  ret
  } // end of method PortableSettingsProvider::setXmlValue

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Code size       7 (0x7)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void Bluegrams.Application.PortableSettingsProviderBase::.ctor()
    IL_0006:  ret
  } // end of method PortableSettingsProvider::.ctor

  .method private hidebysig specialname rtspecialname static 
          void  .cctor() cil managed
  {
    // Code size       11 (0xb)
    .maxstack  8
    IL_0000:  ldstr      "portable.config"
    IL_0005:  stsfld     string Bluegrams.Application.PortableSettingsProvider::'<SettingsFileName>k__BackingField'
    IL_000a:  ret
  } // end of method PortableSettingsProvider::.cctor

  .property string SettingsFileName()
  {
    .get string Bluegrams.Application.PortableSettingsProvider::get_SettingsFileName()
    .set void Bluegrams.Application.PortableSettingsProvider::set_SettingsFileName(string)
  } // end of property PortableSettingsProvider::SettingsFileName
  .property instance string Name()
  {
    .get instance string Bluegrams.Application.PortableSettingsProvider::get_Name()
  } // end of property PortableSettingsProvider::Name
  .property instance string ApplicationSettingsFile()
  {
    .get instance string Bluegrams.Application.PortableSettingsProvider::get_ApplicationSettingsFile()
  } // end of property PortableSettingsProvider::ApplicationSettingsFile
} // end of class Bluegrams.Application.PortableSettingsProvider

.class public abstract auto ansi beforefieldinit Bluegrams.Application.PortableSettingsProviderBase
       extends [System]System.Configuration.SettingsProvider
       implements [System]System.Configuration.IApplicationSettingsProvider
{
  .field private static bool '<AllRoaming>k__BackingField'
  .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
  .field private static string '<SettingsDirectory>k__BackingField'
  .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
  .method public hidebysig specialname static 
          bool  get_AllRoaming() cil managed
  {
    .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    // Code size       6 (0x6)
    .maxstack  8
    IL_0000:  ldsfld     bool Bluegrams.Application.PortableSettingsProviderBase::'<AllRoaming>k__BackingField'
    IL_0005:  ret
  } // end of method PortableSettingsProviderBase::get_AllRoaming

  .method public hidebysig specialname static 
          void  set_AllRoaming(bool 'value') cil managed
  {
    .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    // Code size       7 (0x7)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  stsfld     bool Bluegrams.Application.PortableSettingsProviderBase::'<AllRoaming>k__BackingField'
    IL_0006:  ret
  } // end of method PortableSettingsProviderBase::set_AllRoaming

  .method public hidebysig specialname static 
          string  get_SettingsDirectory() cil managed
  {
    .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    // Code size       6 (0x6)
    .maxstack  8
    IL_0000:  ldsfld     string Bluegrams.Application.PortableSettingsProviderBase::'<SettingsDirectory>k__BackingField'
    IL_0005:  ret
  } // end of method PortableSettingsProviderBase::get_SettingsDirectory

  .method public hidebysig specialname static 
          void  set_SettingsDirectory(string 'value') cil managed
  {
    .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = ( 01 00 00 00 ) 
    // Code size       7 (0x7)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  stsfld     string Bluegrams.Application.PortableSettingsProviderBase::'<SettingsDirectory>k__BackingField'
    IL_0006:  ret
  } // end of method PortableSettingsProviderBase::set_SettingsDirectory

  .method public hidebysig specialname virtual 
          instance string  get_ApplicationName() cil managed
  {
    // Code size       16 (0x10)
    .maxstack  8
    IL_0000:  call       class [mscorlib]System.Reflection.Assembly [mscorlib]System.Reflection.Assembly::GetExecutingAssembly()
    IL_0005:  callvirt   instance class [mscorlib]System.Reflection.AssemblyName [mscorlib]System.Reflection.Assembly::GetName()
    IL_000a:  callvirt   instance string [mscorlib]System.Reflection.AssemblyName::get_Name()
    IL_000f:  ret
  } // end of method PortableSettingsProviderBase::get_ApplicationName

  .method public hidebysig specialname virtual 
          instance void  set_ApplicationName(string 'value') cil managed
  {
    // Code size       1 (0x1)
    .maxstack  8
    IL_0000:  ret
  } // end of method PortableSettingsProviderBase::set_ApplicationName

  .method family hidebysig static void  ApplyProvider(class Bluegrams.Application.PortableSettingsProviderBase provider,
                                                      class [System]System.Configuration.ApplicationSettingsBase[] settingsList) cil managed
  {
    .param [2]
    .custom instance void [mscorlib]System.ParamArrayAttribute::.ctor() = ( 01 00 00 00 ) 
    // Code size       111 (0x6f)
    .maxstack  2
    .locals init (class [System]System.Configuration.ApplicationSettingsBase[] V_0,
             int32 V_1,
             class [System]System.Configuration.ApplicationSettingsBase V_2,
             class [mscorlib]System.Collections.IEnumerator V_3,
             class [mscorlib]System.IDisposable V_4)
    IL_0000:  ldarg.1
    IL_0001:  stloc.0
    IL_0002:  ldc.i4.0
    IL_0003:  stloc.1
    IL_0004:  br.s       IL_0068

    IL_0006:  ldloc.0
    IL_0007:  ldloc.1
    IL_0008:  ldelem.ref
    IL_0009:  stloc.2
    IL_000a:  ldloc.2
    IL_000b:  callvirt   instance class [System]System.Configuration.SettingsProviderCollection [System]System.Configuration.SettingsBase::get_Providers()
    IL_0010:  callvirt   instance void [System.Configuration]System.Configuration.Provider.ProviderCollection::Clear()
    IL_0015:  ldloc.2
    IL_0016:  callvirt   instance class [System]System.Configuration.SettingsProviderCollection [System]System.Configuration.SettingsBase::get_Providers()
    IL_001b:  ldarg.0
    IL_001c:  callvirt   instance void [System.Configuration]System.Configuration.Provider.ProviderCollection::Add(class [System.Configuration]System.Configuration.Provider.ProviderBase)
    IL_0021:  ldloc.2
    IL_0022:  callvirt   instance class [System]System.Configuration.SettingsPropertyCollection [System]System.Configuration.SettingsBase::get_Properties()
    IL_0027:  callvirt   instance class [mscorlib]System.Collections.IEnumerator [System]System.Configuration.SettingsPropertyCollection::GetEnumerator()
    IL_002c:  stloc.3
    .try
    {
      IL_002d:  br.s       IL_0040

      IL_002f:  ldloc.3
      IL_0030:  callvirt   instance object [mscorlib]System.Collections.IEnumerator::get_Current()
      IL_0035:  castclass  [System]System.Configuration.SettingsProperty
      IL_003a:  ldarg.0
      IL_003b:  callvirt   instance void [System]System.Configuration.SettingsProperty::set_Provider(class [System]System.Configuration.SettingsProvider)
      IL_0040:  ldloc.3
      IL_0041:  callvirt   instance bool [mscorlib]System.Collections.IEnumerator::MoveNext()
      IL_0046:  brtrue.s   IL_002f

      IL_0048:  leave.s    IL_005e

    }  // end .try
    finally
    {
      IL_004a:  ldloc.3
      IL_004b:  isinst     [mscorlib]System.IDisposable
      IL_0050:  stloc.s    V_4
      IL_0052:  ldloc.s    V_4
      IL_0054:  brfalse.s  IL_005d

      IL_0056:  ldloc.s    V_4
      IL_0058:  callvirt   instance void [mscorlib]System.IDisposable::Dispose()
      IL_005d:  endfinally
    }  // end handler
    IL_005e:  ldloc.2
    IL_005f:  callvirt   instance void [System]System.Configuration.ApplicationSettingsBase::Reload()
    IL_0064:  ldloc.1
    IL_0065:  ldc.i4.1
    IL_0066:  add
    IL_0067:  stloc.1
    IL_0068:  ldloc.1
    IL_0069:  ldloc.0
    IL_006a:  ldlen
    IL_006b:  conv.i4
    IL_006c:  blt.s      IL_0006

    IL_006e:  ret
  } // end of method PortableSettingsProviderBase::ApplyProvider

  .method public hidebysig virtual instance void 
          Initialize(string name,
                     class [System]System.Collections.Specialized.NameValueCollection config) cil managed
  {
    // Code size       25 (0x19)
    .maxstack  8
    IL_0000:  ldarg.1
    IL_0001:  call       bool [mscorlib]System.String::IsNullOrEmpty(string)
    IL_0006:  brfalse.s  IL_0010

    IL_0008:  ldarg.0
    IL_0009:  callvirt   instance string [System.Configuration]System.Configuration.Provider.ProviderBase::get_Name()
    IL_000e:  starg.s    name
    IL_0010:  ldarg.0
    IL_0011:  ldarg.1
    IL_0012:  ldarg.2
    IL_0013:  call       instance void [System.Configuration]System.Configuration.Provider.ProviderBase::Initialize(string,
                                                                                                                    class [System]System.Collections.Specialized.NameValueCollection)
    IL_0018:  ret
  } // end of method PortableSettingsProviderBase::Initialize

  .method public hidebysig newslot virtual 
          instance class [System]System.Configuration.SettingsPropertyValue 
          GetPreviousVersion(class [System]System.Configuration.SettingsContext context,
                             class [System]System.Configuration.SettingsProperty 'property') cil managed
  {
    // Code size       6 (0x6)
    .maxstack  8
    IL_0000:  newobj     instance void [mscorlib]System.NotImplementedException::.ctor()
    IL_0005:  throw
  } // end of method PortableSettingsProviderBase::GetPreviousVersion

  .method public hidebysig newslot abstract virtual 
          instance void  Reset(class [System]System.Configuration.SettingsContext context) cil managed
  {
  } // end of method PortableSettingsProviderBase::Reset

  .method public hidebysig newslot virtual 
          instance void  Upgrade(class [System]System.Configuration.SettingsContext context,
                                 class [System]System.Configuration.SettingsPropertyCollection properties) cil managed
  {
    // Code size       1 (0x1)
    .maxstack  8
    IL_0000:  ret
  } // end of method PortableSettingsProviderBase::Upgrade

  .method family hidebysig instance bool 
          IsUserScoped(class [System]System.Configuration.SettingsProperty prop) cil managed
  {
    // Code size       95 (0x5f)
    .maxstack  2
    .locals init (class [mscorlib]System.Collections.IDictionaryEnumerator V_0,
             valuetype [mscorlib]System.Collections.DictionaryEntry V_1,
             bool V_2,
             class [mscorlib]System.IDisposable V_3)
    IL_0000:  ldarg.1
    IL_0001:  callvirt   instance class [System]System.Configuration.SettingsAttributeDictionary [System]System.Configuration.SettingsProperty::get_Attributes()
    IL_0006:  callvirt   instance class [mscorlib]System.Collections.IDictionaryEnumerator [mscorlib]System.Collections.Hashtable::GetEnumerator()
    IL_000b:  stloc.0
    .try
    {
      IL_000c:  br.s       IL_0040

      IL_000e:  ldloc.0
      IL_000f:  callvirt   instance object [mscorlib]System.Collections.IEnumerator::get_Current()
      IL_0014:  unbox.any  [mscorlib]System.Collections.DictionaryEntry
      IL_0019:  stloc.1
      IL_001a:  ldloca.s   V_1
      IL_001c:  call       instance object [mscorlib]System.Collections.DictionaryEntry::get_Value()
      IL_0021:  castclass  [mscorlib]System.Attribute
      IL_0026:  callvirt   instance class [mscorlib]System.Type [mscorlib]System.Object::GetType()
      IL_002b:  ldtoken    [System]System.Configuration.UserScopedSettingAttribute
      IL_0030:  call       class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle)
      IL_0035:  call       bool [mscorlib]System.Type::op_Equality(class [mscorlib]System.Type,
                                                                   class [mscorlib]System.Type)
      IL_003a:  brfalse.s  IL_0040

      IL_003c:  ldc.i4.1
      IL_003d:  stloc.2
      IL_003e:  leave.s    IL_005d

      IL_0040:  ldloc.0
      IL_0041:  callvirt   instance bool [mscorlib]System.Collections.IEnumerator::MoveNext()
      IL_0046:  brtrue.s   IL_000e

      IL_0048:  leave.s    IL_005b

    }  // end .try
    finally
    {
      IL_004a:  ldloc.0
      IL_004b:  isinst     [mscorlib]System.IDisposable
      IL_0050:  stloc.3
      IL_0051:  ldloc.3
      IL_0052:  brfalse.s  IL_005a

      IL_0054:  ldloc.3
      IL_0055:  callvirt   instance void [mscorlib]System.IDisposable::Dispose()
      IL_005a:  endfinally
    }  // end handler
    IL_005b:  ldc.i4.0
    IL_005c:  ret

    IL_005d:  ldloc.2
    IL_005e:  ret
  } // end of method PortableSettingsProviderBase::IsUserScoped

  .method family hidebysig instance bool 
          IsRoaming(class [System]System.Configuration.SettingsProperty prop) cil managed
  {
    // Code size       104 (0x68)
    .maxstack  2
    .locals init (class [mscorlib]System.Collections.IDictionaryEnumerator V_0,
             valuetype [mscorlib]System.Collections.DictionaryEntry V_1,
             bool V_2,
             class [mscorlib]System.IDisposable V_3)
    IL_0000:  call       bool Bluegrams.Application.PortableSettingsProviderBase::get_AllRoaming()
    IL_0005:  brfalse.s  IL_0009

    IL_0007:  ldc.i4.1
    IL_0008:  ret

    IL_0009:  ldarg.1
    IL_000a:  callvirt   instance class [System]System.Configuration.SettingsAttributeDictionary [System]System.Configuration.SettingsProperty::get_Attributes()
    IL_000f:  callvirt   instance class [mscorlib]System.Collections.IDictionaryEnumerator [mscorlib]System.Collections.Hashtable::GetEnumerator()
    IL_0014:  stloc.0
    .try
    {
      IL_0015:  br.s       IL_0049

      IL_0017:  ldloc.0
      IL_0018:  callvirt   instance object [mscorlib]System.Collections.IEnumerator::get_Current()
      IL_001d:  unbox.any  [mscorlib]System.Collections.DictionaryEntry
      IL_0022:  stloc.1
      IL_0023:  ldloca.s   V_1
      IL_0025:  call       instance object [mscorlib]System.Collections.DictionaryEntry::get_Value()
      IL_002a:  castclass  [mscorlib]System.Attribute
      IL_002f:  callvirt   instance class [mscorlib]System.Type [mscorlib]System.Object::GetType()
      IL_0034:  ldtoken    [System]System.Configuration.SettingsManageabilityAttribute
      IL_0039:  call       class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle)
      IL_003e:  call       bool [mscorlib]System.Type::op_Equality(class [mscorlib]System.Type,
                                                                   class [mscorlib]System.Type)
      IL_0043:  brfalse.s  IL_0049

      IL_0045:  ldc.i4.1
      IL_0046:  stloc.2
      IL_0047:  leave.s    IL_0066

      IL_0049:  ldloc.0
      IL_004a:  callvirt   instance bool [mscorlib]System.Collections.IEnumerator::MoveNext()
      IL_004f:  brtrue.s   IL_0017

      IL_0051:  leave.s    IL_0064

    }  // end .try
    finally
    {
      IL_0053:  ldloc.0
      IL_0054:  isinst     [mscorlib]System.IDisposable
      IL_0059:  stloc.3
      IL_005a:  ldloc.3
      IL_005b:  brfalse.s  IL_0063

      IL_005d:  ldloc.3
      IL_005e:  callvirt   instance void [mscorlib]System.IDisposable::Dispose()
      IL_0063:  endfinally
    }  // end handler
    IL_0064:  ldc.i4.0
    IL_0065:  ret

    IL_0066:  ldloc.2
    IL_0067:  ret
  } // end of method PortableSettingsProviderBase::IsRoaming

  .method family hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Code size       7 (0x7)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [System]System.Configuration.SettingsProvider::.ctor()
    IL_0006:  ret
  } // end of method PortableSettingsProviderBase::.ctor

  .method private hidebysig specialname rtspecialname static 
          void  .cctor() cil managed
  {
    // Code size       22 (0x16)
    .maxstack  8
    IL_0000:  ldc.i4.0
    IL_0001:  stsfld     bool Bluegrams.Application.PortableSettingsProviderBase::'<AllRoaming>k__BackingField'
    IL_0006:  call       class [mscorlib]System.AppDomain [mscorlib]System.AppDomain::get_CurrentDomain()
    IL_000b:  callvirt   instance string [mscorlib]System.AppDomain::get_BaseDirectory()
    IL_0010:  stsfld     string Bluegrams.Application.PortableSettingsProviderBase::'<SettingsDirectory>k__BackingField'
    IL_0015:  ret
  } // end of method PortableSettingsProviderBase::.cctor

  .property bool AllRoaming()
  {
    .get bool Bluegrams.Application.PortableSettingsProviderBase::get_AllRoaming()
    .set void Bluegrams.Application.PortableSettingsProviderBase::set_AllRoaming(bool)
  } // end of property PortableSettingsProviderBase::AllRoaming
  .property string SettingsDirectory()
  {
    .get string Bluegrams.Application.PortableSettingsProviderBase::get_SettingsDirectory()
    .set void Bluegrams.Application.PortableSettingsProviderBase::set_SettingsDirectory(string)
  } // end of property PortableSettingsProviderBase::SettingsDirectory
  .property instance string ApplicationName()
  {
    .get instance string Bluegrams.Application.PortableSettingsProviderBase::get_ApplicationName()
    .set instance void Bluegrams.Application.PortableSettingsProviderBase::set_ApplicationName(string)
  } // end of property PortableSettingsProviderBase::ApplicationName
} // end of class Bluegrams.Application.PortableSettingsProviderBase


// =============================================================

// *********** DISASSEMBLY COMPLETE ***********************
// ADVERTENCIA: se ha creado el archivo de recursos de Win32 C:\Datos\Nextcloud\FFmpeg Batch\ffmpeg_batch-master\packages\PortableSettingsProvider.0.2.5\lib\net45\PortableSettingsProvider.res
