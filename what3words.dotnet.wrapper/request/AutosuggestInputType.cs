public enum AutosuggestInputType {
    /**
     * Text typed by a user, e.g.'index.home.raf'. Default setting.
     * Expects exactly two dots and no whitespace in the input.
     */
    TEXT,
    /**
     * JSON from Nuance VoCon&reg; Hybrid.
     * This should only be used with grammars provided by what3words ltd;
     * using other grammars will cause run-time exceptions.
     * Language detection is disabled, so lang must be set for non-English input.
     */
    VOCON_HYBRID,
    /**
     * Text from the Nuance server at https://dictation.nuancemobility.net/NMDPAsrCmdServlet/dictation -
     * please contact Nuance before attempting to use this server.
     * Use with text from any other source is unsupported and results may not be as expected.
     * Does not handle text in languages in which words are not separated by spaces, e.g. Chinese.
     * Language detection is disabled, so lang must be set for non-English input.
     */
    NMDP_ASR,
    /**
     * 
     * 
     * Text output from speech recognition software. E.g. "index home raft".
     * This input type handles spaces between words.
     * Users should not pronounce 'dot' when speaking a 3 word address.
     * Does not handle text in languages in which words are not separated by spaces, e.g. Chinese.
     * Language detection is disabled, so lang must be set.
     */
    GENERIC_VOICE,
    /**
     * Text from Speechmatics voice recognition.
     * Use with text from any other source is unsupported and results may not be as expected.
     * This should only be used with grammars provided by what3words ltd;
     * using other grammars will cause run-time exceptions.
     * Language detection is disabled, so language must be set.
     */
    SPEECHMATICS
}