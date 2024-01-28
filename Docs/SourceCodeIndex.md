# Source Code Index

## Original Source Code Index

* [Lockheed 1103AF Operational System Listing](./pdfs/Lockheed_1103AF_Operational_System_Listing.pdf). Listings include an apparently complete assembler and large number of service routines. Service routines use tape units, card reader and drum. Some small service routines are missing. There is an apparently complete trace program, a variety of software floating point routines, a "Complex Floating Point Package" listing is one of the few programs that uses machine floating point instructions, and some moderately involved IO routines.
* [Lockheed FAP Package](./pdfs/1103A__Lockheed_Package.pdf). Large listing without any documentation, unsure what it's purpose is.
* [USEful Routines](./pdfs/USEful_Routines.pdf) . First listing on page 45 of some card read/punch routines with decimal/binary conversion and scaling. Page 79 starts listings of acceptance tests for tapes and drums. Octal card dump routine on page 169. Page 185 starts the Boeing Service Routine library with a set of basic routines. Finally there's an double precision floating point routine on page 240.
* [Unicode compiler](./pdfs/Unicode/U1451_Unicode_Preliminary_Reference_Manual_1957.pdf). 1900 pages of documentation, flow-charts and listings of the Unicode compiler for the 1103A. However the listings are not in a machine readable format and are all spread out, one routine at a time and uses a notation I can't quite figure out. It would a massive amount of work to stitch together a working compiler from the listings, if the listings are even complete. How is the data arranged on tape? And many similar questions.