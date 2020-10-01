var VersionNumber = function (data) {

    var number = '',
        displayVersion = '',
        caption = '',
        releaseDate = null,
        releaseType = null,
        versionBin = null
        ;

    var majorShift = 281474976710656,
        minorShift = 4294967296,
        buildShift = 65536;


    if (data) {
        number = data.number;
        caption = data.caption;
        displayVersion = data.displayVersion;
        releaseDate = data.releaseDate;
        releaseType = data.releaseType;
        versionBin = toBin(parse(data.number));
    }


    function parse(versionString) {
        var ver = {
            major: 0,
            minor: 0,
            build: 0,
            revision: 0
        };

        if (!versionString || versionString.length === 0) {
            return ver;
        }

        var parts = versionString.split('.');
        if (parts.length > 3) ver.revision = parseInt(parts[3], 10);
        if (parts.length > 2) ver.build = parseInt(parts[2], 10);
        if (parts.length > 1) ver.minor = parseInt(parts[1], 10);
        ver.major = parseInt(parts[0], 10);

        return ver;
    }

    // binary representation of the version number; used in ordering
    function toBin(version) {
        return Math.max(version.major, 0) * majorShift
            + Math.max(version.minor, 0) * minorShift
            + Math.max(version.build, 0) * buildShift
            + Math.max(version.revision);
    }

    function compare(other) {
        if (!other) {
            return false;
        }

        var otherBin = toBin(parse(other));

        return otherBin > versionBin;
    }

    return {
        number: number,
        displayVersion: displayVersion,
        caption: caption,
        releaseDate: releaseDate,
        releaseType: releaseType,
        versionBin: versionBin,
        parse: parse,
        compare: compare,
        toBin: toBin
    };
};