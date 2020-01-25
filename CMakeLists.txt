cmake_minimum_required(VERSION 3.8.0)
project(tds)

# WINDOWS
if(WIN32)
   set(VCPKG ${CMAKE_SOURCE_DIR}/_vcpkg)

   # install vcpkg
   if (NOT IS_DIRECTORY ${VCPKG})
      message("vcpkg does not exist. installing...")
      execute_process(
         COMMAND git.exe clone https://github.com/Microsoft/vcpkg.git ${VCPKG}
         WORKING_DIRECTORY ${CMAKE_SOURCE_DIR}
      )
      execute_process(
         COMMAND bootstrap-vcpkg.bat
         WORKING_DIRECTORY ${VCPKG}
      )
   endif()

   ## install freetds:x86-windows
   #if (NOT IS_DIRECTORY ${VCPKG}/packages/freetds_x86-windows)
   #   execute_process(
   #      COMMAND vcpkg install freetds:x86-windows --overlay-ports=../custom-ports
   #      WORKING_DIRECTORY ${VCPKG}
   #   )
   #endif()
   ## generate tds.def
   #if (NOT EXISTS ${VCPKG}/packages/freetds_x86-windows/lib/tds.def)
   #   message("x86 tds.def. generating...")
   #   execute_process(
   #      COMMAND dotnet run --project ./DefGen x86 ${VCPKG}/packages/freetds_x86-windows/lib/tds.lib ${VCPKG}/packages/freetds_x86-windows/lib/tdssrv.lib
   #      WORKING_DIRECTORY ${CMAKE_SOURCE_DIR}
   #   )
   #endif()

   # install freetds:x64-windows
   if (NOT IS_DIRECTORY ${VCPKG}/packages/freetds_x64-windows)
      execute_process(
         COMMAND vcpkg install freetds:x64-windows --overlay-ports=../custom-ports
         WORKING_DIRECTORY ${VCPKG}
      )
   endif()
   # generate tds.def
   if (NOT EXISTS ${VCPKG}/packages/freetds_x64-windows/lib/tds.def)
      message("x64 tds.def. generating...")
      execute_process(
         COMMAND dotnet run --project ./DefGen x64 ${VCPKG}/packages/freetds_x64-windows/lib/tds.lib ${VCPKG}/packages/freetds_x64-windows/lib/tdssrv.lib
         WORKING_DIRECTORY ${CMAKE_SOURCE_DIR}
      )
   endif()
endif()

# tds
add_library(tds SHARED empty.cc ${VCPKG}/packages/freetds_${BITNESS}-windows/lib/tds.def)
target_link_libraries(tds
   Ws2_32.lib
   ${VCPKG}/packages/freetds_${BITNESS}-windows/lib/tds.lib
   ${VCPKG}/packages/freetds_${BITNESS}-windows/lib/tdssrv.lib
   ${VCPKG}/packages/freetds_${BITNESS}-windows/lib/tdsutils.lib
   ${VCPKG}/packages/freetds_${BITNESS}-windows/lib/replacements.lib
   ${VCPKG}/packages/openssl-windows_${BITNESS}-windows/lib/libcrypto.lib
   ${VCPKG}/packages/openssl-windows_${BITNESS}-windows/lib/libssl.lib)
