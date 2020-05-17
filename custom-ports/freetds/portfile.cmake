include(vcpkg_common_functions)

vcpkg_download_distfile(ARCHIVE
    URLS "https://www.freetds.org/files/stable/freetds-1.1.36.tar.bz2"
    FILENAME "freetds-1.1.36.tar.bz2"
    SHA512 4f657cba4cec1490364f08ab9b84760d9c95c530094758b2166409c0c268aceb273681dff6abebf7b4914a3a44db5ed083bae347a60af5121b5030d16b4d6971
)

vcpkg_extract_source_archive_ex(
    OUT_SOURCE_PATH SOURCE_PATH
    ARCHIVE ${ARCHIVE}
    PATCHES
        crypt32.patch
)
file(INSTALL ${CURRENT_PORT_DIR}/pool/user.c DESTINATION ${SOURCE_PATH}/src/pool)
file(INSTALL ${CURRENT_PORT_DIR}/server/login.c DESTINATION ${SOURCE_PATH}/src/server)
file(INSTALL ${CURRENT_PORT_DIR}/server/query.c DESTINATION ${SOURCE_PATH}/src/server)
file(INSTALL ${CURRENT_PORT_DIR}/server/server.c DESTINATION ${SOURCE_PATH}/src/server)
file(INSTALL ${CURRENT_PORT_DIR}/server/unittest.c DESTINATION ${SOURCE_PATH}/src/server)
file(INSTALL ${CURRENT_PORT_DIR}/server/server.h DESTINATION ${SOURCE_PATH}/include/freetds)

set(BUILD_freetds_openssl OFF)
if("openssl" IN_LIST FEATURES)
    set(BUILD_freetds_openssl ON)
endif()

vcpkg_configure_cmake(
    SOURCE_PATH ${SOURCE_PATH}
    PREFER_NINJA
    OPTIONS
        -DWITH_OPENSSL=${BUILD_freetds_openssl}
)

vcpkg_install_cmake()
vcpkg_copy_pdbs()

file(REMOVE_RECURSE ${CURRENT_PACKAGES_DIR}/debug/include)
file(REMOVE ${CURRENT_PACKAGES_DIR}/bin/bsqldb.exe)
file(REMOVE ${CURRENT_PACKAGES_DIR}/bin/bsqlodbc.exe)
file(REMOVE ${CURRENT_PACKAGES_DIR}/bin/datacopy.exe)
file(REMOVE ${CURRENT_PACKAGES_DIR}/bin/defncopy.exe)
file(REMOVE ${CURRENT_PACKAGES_DIR}/bin/freebcp.exe)
file(REMOVE ${CURRENT_PACKAGES_DIR}/bin/tdspool.exe)
file(REMOVE ${CURRENT_PACKAGES_DIR}/bin/tsql.exe)
file(REMOVE ${CURRENT_PACKAGES_DIR}/bin/bsqldb)
file(REMOVE ${CURRENT_PACKAGES_DIR}/bin/bsqlodbc)
file(REMOVE ${CURRENT_PACKAGES_DIR}/bin/datacopy)
file(REMOVE ${CURRENT_PACKAGES_DIR}/bin/defncopy)
file(REMOVE ${CURRENT_PACKAGES_DIR}/bin/freebcp)
file(REMOVE ${CURRENT_PACKAGES_DIR}/bin/tdspool)
file(REMOVE ${CURRENT_PACKAGES_DIR}/bin/tsql)
file(REMOVE ${CURRENT_PACKAGES_DIR}/debug/bin/bsqldb.exe)
file(REMOVE ${CURRENT_PACKAGES_DIR}/debug/bin/bsqlodbc.exe)
file(REMOVE ${CURRENT_PACKAGES_DIR}/debug/bin/datacopy.exe)
file(REMOVE ${CURRENT_PACKAGES_DIR}/debug/bin/defncopy.exe)
file(REMOVE ${CURRENT_PACKAGES_DIR}/debug/bin/freebcp.exe)
file(REMOVE ${CURRENT_PACKAGES_DIR}/debug/bin/tdspool.exe)
file(REMOVE ${CURRENT_PACKAGES_DIR}/debug/bin/tsql.exe)
file(REMOVE ${CURRENT_PACKAGES_DIR}/debug/bin/bsqldb)
file(REMOVE ${CURRENT_PACKAGES_DIR}/debug/bin/bsqlodbc)
file(REMOVE ${CURRENT_PACKAGES_DIR}/debug/bin/datacopy)
file(REMOVE ${CURRENT_PACKAGES_DIR}/debug/bin/defncopy)
file(REMOVE ${CURRENT_PACKAGES_DIR}/debug/bin/freebcp)
file(REMOVE ${CURRENT_PACKAGES_DIR}/debug/bin/tdspool)
file(REMOVE ${CURRENT_PACKAGES_DIR}/debug/bin/tsql)

if(VCPKG_LIBRARY_LINKAGE STREQUAL static)
    file(REMOVE_RECURSE ${CURRENT_PACKAGES_DIR}/bin ${CURRENT_PACKAGES_DIR}/debug/bin)
endif()

file(INSTALL ${SOURCE_PATH}/COPYING DESTINATION ${CURRENT_PACKAGES_DIR}/share/${PORT} RENAME copyright)
